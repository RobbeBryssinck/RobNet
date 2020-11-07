using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BotnetJobs.Data.Repository.v1;
using BotnetJobs.Domain.Entities;
using MediatR;

namespace BotnetJobs.Service.v1.Query
{
    public class GetBotnetJobByBotnetIdQueryHandler : IRequestHandler<GetBotnetJobByBotnetIdQuery, BotnetJob>
    {
        private readonly IBotnetJobRepository _botnetJobRepository;

        public GetBotnetJobByBotnetIdQueryHandler(IBotnetJobRepository botnetJobRepository)
        {
            _botnetJobRepository = botnetJobRepository;
        }

        public async Task<BotnetJob> Handle(GetBotnetJobByBotnetIdQuery request, CancellationToken cancellationToken)
        {
            return await _botnetJobRepository.GetBotnetJobByBotnetIdAsync(request.BotnetId, cancellationToken);
        }
    }
}
