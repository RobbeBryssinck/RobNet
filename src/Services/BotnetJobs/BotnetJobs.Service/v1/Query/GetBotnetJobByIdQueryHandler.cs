using System.Threading;
using System.Threading.Tasks;
using BotnetJobs.Data.Repository.v1;
using BotnetJobs.Domain.Entities;
using MediatR;

namespace BotnetJobs.Service.v1.Query
{
    public class GetBotnetJobByIdQueryHandler : IRequestHandler<GetBotnetJobByIdQuery, BotnetJob>
    {
        private readonly IBotnetJobRepository _botnetJobRepository;

        public GetBotnetJobByIdQueryHandler(IBotnetJobRepository botnetJobRepository)
        {
            _botnetJobRepository = botnetJobRepository;
        }

        public async Task<BotnetJob> Handle(GetBotnetJobByIdQuery request, CancellationToken cancellationToken)
        {
            return await _botnetJobRepository.GetBotnetJobByIdAsync(request.Id, cancellationToken);
        }
    }
}
