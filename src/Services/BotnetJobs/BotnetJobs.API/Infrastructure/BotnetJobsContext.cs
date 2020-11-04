using BotnetJobs.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BotnetJobs.API.Infrastructure
{
    public class BotnetJobsContext : DbContext
    {
        public BotnetJobsContext(DbContextOptions<BotnetJobsContext> options) : base(options)
        {
        }

        public DbSet<BotnetJob> BotnetJobs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
