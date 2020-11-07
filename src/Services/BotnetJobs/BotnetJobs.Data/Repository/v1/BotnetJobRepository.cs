using System;
using System.Threading;
using System.Threading.Tasks;
using BotnetJobs.Data.Database;
using BotnetJobs.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BotnetJobs.Data.Repository.v1
{
    public class BotnetJobRepository : Repository<BotnetJob>, IBotnetJobRepository
    {
        public BotnetJobRepository(BotnetJobContext context) : base(context)
        {
        }

        public async Task<BotnetJob> GetBotnetJobByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.BotnetJob.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<BotnetJob> GetBotnetJobByBotnetIdAsync(int botnetId, CancellationToken cancellationToken)
        {
            return await _context.BotnetJob.FirstOrDefaultAsync(x => x.BotnetId == botnetId, cancellationToken);
        }
    }
}
