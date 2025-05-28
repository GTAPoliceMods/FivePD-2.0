// Copyright (c) GTAPoliceMods LLC. All Rights Reserved.
//
// All code and assets contained in this file are STRICTLY
// the intellectual property of GTAPoliceMods unless otherwise
// noted by a license provided. Do not copy or redistribute.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FivePD.Gamemode.Server.Database
{
    /// <summary>
    /// Design-time context factory used only for migration generation.
    /// </summary>
    public class DatabaseContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
    {
        /// <inheritdoc/>
        public DatabaseContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
            optionsBuilder.UseMySql("server=;port=;database=;user=;password=;");

            return new DatabaseContext(optionsBuilder.Options);
        }
    }
}