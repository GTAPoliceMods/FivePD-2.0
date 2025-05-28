// Copyright (c) GTAPoliceMods LLC. All Rights Reserved.
//
// All code and assets contained in this file are STRICTLY
// the intellectual property of GTAPoliceMods unless otherwise
// noted by a license provided. Do not copy or redistribute.

using System;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace FivePD.Gamemode.Server.Extensions
{
    /**
     * <summary>Holds extension methods for <see cref="IServiceCollection"/>.</summary>
     */
    public static class Services
    {
        /**
         * <summary>Adds a new Serilog logger to the provided <see cref="IServiceCollection"/>.</summary>
         * <param name="services">The service collection to use.</param>
         * <param name="configuration">A <see cref="LoggerConfiguration"/> that will be used to create the new logger.</param>
         * <returns>The service collection with added logger.</returns>
         */
        public static IServiceCollection AddSerilogServices(this IServiceCollection services, LoggerConfiguration configuration)
        {
            Log.Logger = configuration.CreateLogger();
            AppDomain.CurrentDomain.ProcessExit += (s, e) => Log.CloseAndFlush();

            return services.AddSingleton(Log.Logger);
        }
    }
}