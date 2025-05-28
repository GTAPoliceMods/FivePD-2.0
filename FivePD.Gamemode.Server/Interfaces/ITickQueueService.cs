// Copyright (c) GTAPoliceMods LLC. All Rights Reserved.
//
// All code and assets contained in this file are STRICTLY
// the intellectual property of GTAPoliceMods unless otherwise
// noted by a license provided. Do not copy or redistribute.

using System;
using System.Threading.Tasks;

namespace FivePD.Gamemode.Server.Interfaces
{
    /**
     * <summary>Handles invocation on a tick by tick basis.
     * Useful for calling natives on a background thread when
     * we don't necessarily need an immediate result.</summary>
     */
    public interface ITickQueueService
    {
        /**
         * <summary>Works through the queue, executing
         * tasks and returning their values.</summary>
         * <returns>A <see cref="Task" /> which completes when
         * the tick is done being operated on.</returns>
         */
        public Task OnTick();

        /**
         * <summary>Pushes an action to the tick queue,
         * and requests that the system runs a tick
         * for the current resource ASAP.</summary>
         * <param name="action">An action to execute and complete upon execution.</param>
         * <returns>A task which completes upon execution.</returns>
         */
        public Task Enqueue(Action action);

        /**
         * <summary>See <see cref="Enqueue"/>.</summary>
         * <typeparam name="T">The type to return.</typeparam>
         * <param name="action">An action to execute and complete upon execution.</param>
         * <returns>A task which completes upon execution.</returns>
         */
        public Task<T> Enqueue<T>(Func<T> action);
    }
}