using Microsoft.EntityFrameworkCore;

namespace BotnetAPI.Models
{
    public class BotnetContext : DbContext
    {
        public BotnetContext(DbContextOptions<BotnetContext> options) : base(options)
        {
        }

        public DbSet<Bot> Bots { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Bot>().ToTable("Bot");
        }
    }
}
