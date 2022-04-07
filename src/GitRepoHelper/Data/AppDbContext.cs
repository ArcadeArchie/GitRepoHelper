using GitRepoHelper.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitRepoHelper.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<WatchedPath> WatchedPaths { get; set; } = null!;

        public AppDbContext() { }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

            try
            {
                var pendingMigs = this.Database.GetPendingMigrations();
                if (pendingMigs.Any())
                    this.Database.Migrate();
            }
            catch (System.Exception)
            {
                //AppLogger.Instance.LogError(ex.Message, ex);
                throw;
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //needed for creating & applying Migrations through CLI and VS
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlite("Filename=app.db");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
