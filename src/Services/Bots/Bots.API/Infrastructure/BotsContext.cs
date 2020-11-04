using Bots.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bots.API.Infrastructure
{
    public class BotsContext : DbContext
    {
        public BotsContext(DbContextOptions<BotsContext> options) : base(options)
        {
        }

        public DbSet<Bot> Bots { get; set; }
        public DbSet<Botnet> Botnets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
