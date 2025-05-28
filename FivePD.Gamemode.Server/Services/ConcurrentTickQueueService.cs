// Copyright (c) GTAPoliceMods LLC. All Rights Reserved.
//
// All code and assets contained in this file are STRICTLY
// the intellectual property of GTAPoliceMods unless otherwise
// noted by a license provided. Do not copy or redistribute.

using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using FivePD.Gamemode.Server.Interfaces;
using Cfx = CitizenFX.Core.Native;

namespace FivePD.Gamemode.Server.Services
{
    /**
     * <inheritdoc />
     */
    public class ConcurrentTickQueueService : ITickQueueService
    {
        // Inspiration from https://github.com/citizenfx/fivem/blob/f4befbfee415e8f7aea486b61a9cdbc30408cf09/ext/webadmin/server/HttpServer.cs
        private readonly string _resourceName = Cfx.API.GetCurrentResourceName();

        private ConcurrentQueue<Tuple<Action, TaskCompletionSource<int>>> TickQueue { get; } = new ConcurrentQueue<Tuple<Action, TaskCompletionSource<int>>>();

        /**
         * <inheritdoc />
         */
        public Task OnTick()
        {
            while (this.TickQueue.TryDequeue(out var call))
            {
                try
                {
                    call.Item1();

                    call.Item2.SetResult(0);
                }
                catch (Exception e)
                {
                    call.Item2.SetException(e);
                }
            }

            return Task.CompletedTask;
        }

        /**
         * <inheritdoc />
         */
        public Task Enqueue(Action action)
        {
            var tcs = new TaskCompletionSource<int>();

            this.TickQueue.Enqueue(Tuple.Create(action, tcs));

            Cfx.API.ScheduleResourceTick(this._resourceName);

            return tcs.Task;
        }

        /**
         * <inheritdoc />
         */
        public async Task<T> Enqueue<T>(Func<T> action)
        {
            var refHolder = new ReferenceHolder<T>();

            await this.Enqueue(() =>
            {
                refHolder.Value = action();
            });

            return refHolder.Value;
        }

        private sealed class ReferenceHolder<T>
        {
            public T Value { get; set; }
        }
    }
}