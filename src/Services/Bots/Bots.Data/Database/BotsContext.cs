using Bots.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bots.Data.Database
{
    public class BotsContext : DbContext
    {
        public BotsContext(DbContextOptions<BotsContext> options) : base(options)
        {
        }

        public DbSet<Bot> Bots { get; set; }
        public DbSet<Botnet> Botnets { get; set; }
    }
}
