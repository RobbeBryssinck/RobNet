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
    public class GetBotsByBotnetIdSlicedQueryHandler : IRequestHandler<GetBotsByBotnetIdSlicedQuery, List<Bot>>
    {
        private readonly IBotRepository _botRepository;

        public GetBotsByBotnetIdSlicedQueryHandler(IBotRepository botRepository)
        {
            _botRepository = botRepository;
        }

        public async Task<List<Bot>> Handle(GetBotsByBotnetIdSlicedQuery request, CancellationToken cancellationToken)
        {
            return await _botRepository.GetBotsByBotnetIdAsync(request.BotnetId, request.PageSize, request.PageIndex,
                cancellationToken);
        }
    }
}
