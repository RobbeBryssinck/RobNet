using System.Threading;
using System.Threading.Tasks;
using Bots.Data.Repository.v1;
using Bots.Domain.Entities;
using MediatR;

namespace Bots.Service.Query
{
    public class GetBotByIdQueryHandler : IRequestHandler<GetBotByIdQuery, Bot>
    {
        private readonly IBotRepository _botRepository;

        public GetBotByIdQueryHandler(IBotRepository botRepository)
        {
            _botRepository = botRepository;
        }

        public async Task<Bot> Handle(GetBotByIdQuery request, CancellationToken cancellationToken)
        {
            return await _botRepository.GetBotByIdAsync(request.Id, cancellationToken);
        }
    }
}
