using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Bots.Domain.Entities;

namespace Bots.Data.Repository.v1
{
    public interface IBotnetRepository : IRepository<Botnet>
    {
        Task<Botnet> GetBotnetByIdAsync(int id, CancellationToken cancellationToken);
    }
}
