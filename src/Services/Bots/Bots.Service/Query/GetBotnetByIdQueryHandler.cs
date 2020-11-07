using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Bots.Data.Repository.v1;
using Bots.Domain.Entities;
using MediatR;

namespace Bots.Service.Query
{
    public class GetBotnetByIdQueryHandler : IRequestHandler<GetBotnetByIdQuery, Botnet>
    {
        private readonly IBotnetRepository _botnetRepository;

        public GetBotnetByIdQueryHandler(IBotnetRepository botnetRepository)
        {
            _botnetRepository = botnetRepository;
        }

        public async Task<Botnet> Handle(GetBotnetByIdQuery request, CancellationToken cancellationToken)
        {
            return await _botnetRepository.GetBotnetByIdAsync(request.Id, cancellationToken);
        }
    }
}
