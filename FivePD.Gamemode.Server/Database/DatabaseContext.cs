// Copyright (c) GTAPoliceMods LLC. All Rights Reserved.
//
// All code and assets contained in this file are STRICTLY
// the intellectual property of GTAPoliceMods unless otherwise
// noted by a license provided. Do not copy or redistribute.

using FivePD.Gamemode.Server.Interfaces;
using FivePD.Gamemode.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace FivePD.Gamemode.Server.Database
{
    /// <summary>
    /// Provides access to the underlying database implementation.
    /// Most modifications to the database should be handled through
    /// abstractions in other areas of the codebase. Addon developers
    /// should rarely, if ever, have to access this class.
    /// </summary>
    public class DatabaseContext : DbContext
    {
        private readonly IConfigService _configService;

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseContext"/> class.
        /// </summary>
        /// <param name="options">The <see cref="DbContextOptions"/> to use.</param>
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseContext"/> class.
        /// </summary>
        /// <param name="options">asd.</param>
        /// <param name="configService">The <see cref="IConfigService"/> to use.</param>
        public DatabaseContext(DbContextOptions<DatabaseContext> options, IConfigService configService)
            : base(options)
        {
            this._configService = configService;
        }

        /// <summary>
        /// Gets or sets the users table.
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Gets or sets departments table.
        /// </summary>
        public DbSet<Department> Departments { get; set; }

        /// <inheritdoc />
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = this._configService?.GetDatabaseConfig();
            if (config != null)
            {
                optionsBuilder.UseMySql(config.ToString());
            }
        }

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>()
                .HasMany(department => department.Users)
                .WithOne(user => user.Department);
        }
    }
}