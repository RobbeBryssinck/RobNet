using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Bots.Data.Database;
using Bots.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bots.Data.Repository.v1
{
    public class BotRepository : Repository<Bot>, IBotRepository
    {
        public BotRepository(BotsContext context) : base(context)
        {
        }

        public async Task<Bot> GetBotByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Bots.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<List<Bot>> GetBotsByBotnetIdAsync(int botnetId, int pageSize, int pageIndex, CancellationToken cancellationToken)
        {
            return await _context.Bots
                .Where(x => x.BotnetId == botnetId)
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync(cancellationToken);
        }
    }
}
