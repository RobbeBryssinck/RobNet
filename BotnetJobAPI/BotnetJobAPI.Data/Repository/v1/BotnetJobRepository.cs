using BotnetJobAPI.Data.Database;
using BotnetJobAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BotnetJobAPI.Data.Repository.v1
{
    public class BotnetJobRepository  : Repository<BotnetJob>, IBotnetJobRepository
    {
        public BotnetJobRepository(BotnetJobContext context) : base(context)
        {
        }

        public async Task<BotnetJob> GetBotnetJobByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.BotnetJob.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }
    }
}
