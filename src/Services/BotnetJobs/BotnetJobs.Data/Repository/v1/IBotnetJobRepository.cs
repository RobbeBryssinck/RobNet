using BotnetJobs.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BotnetJobs.Data.Repository.v1
{
    public interface IBotnetJobRepository : IRepository<BotnetJob>
    {
        Task<BotnetJob> GetBotnetJobByIdAsync(int id, CancellationToken cancellationToken);
        Task<BotnetJob> GetBotnetJobByBotnetIdAsync(int botnetId, CancellationToken cancellationToken);
    }
}
