using System.Threading;
using System.Threading.Tasks;
using Bots.Data.Database;
using Bots.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bots.Data.Repository.v1
{
    public class BotnetRepository : Repository<Botnet>, IBotnetRepository
    {
        public BotnetRepository(BotsContext context) : base(context)
        {
        }

        public async Task<Botnet> GetBotnetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Botnets.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }
    }
}
