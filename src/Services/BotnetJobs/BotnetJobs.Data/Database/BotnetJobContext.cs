using BotnetJobs.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BotnetJobs.Data.Database
{
    public class BotnetJobContext : DbContext
    {
        public BotnetJobContext(DbContextOptions<BotnetJobContext> options) : base(options)
        {
        }

        public DbSet<BotnetJob> BotnetJob { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<BotnetJob>().ToTable("BotnetJob");
        }
    }
}
