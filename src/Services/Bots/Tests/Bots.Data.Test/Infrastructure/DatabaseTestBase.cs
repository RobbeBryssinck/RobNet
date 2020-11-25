using System;
using Bots.Data.Database;
using Microsoft.EntityFrameworkCore;

namespace Bots.Data.Test.Infrastructure
{
    public class DatabaseTestBase : IDisposable
    {
        protected readonly BotsContext _context;

        public DatabaseTestBase()
        {
            var options = new DbContextOptionsBuilder<BotsContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            _context = new BotsContext(options);
            _context.Database.EnsureCreated();
            DatabaseInitializer.Initialize(_context);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
