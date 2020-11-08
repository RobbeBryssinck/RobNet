using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Bots.Domain.Entities;

namespace Bots.Data.Repository.v1
{
    public interface IBotRepository : IRepository<Bot>
    {
        Task<Bot> GetBotByIdAsync(int id, CancellationToken cancellationToken);

        Task<List<Bot>> GetBotsByBotnetIdAsync(int botnetId, int pageSize, int pageIndex, CancellationToken cancellationToken);
    }
}
