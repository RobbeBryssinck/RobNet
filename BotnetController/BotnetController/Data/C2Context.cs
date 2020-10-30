using BotnetController.Logic;
using Microsoft.EntityFrameworkCore;

namespace BotnetController.Data
{
    public class C2Context : DbContext
    {
        public C2Context(DbContextOptions<C2Context> options) : base(options)
        {
        }

        public DbSet<C2Job> C2Jobs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<C2Job>().ToTable("C2Job");
        }
    }
}
