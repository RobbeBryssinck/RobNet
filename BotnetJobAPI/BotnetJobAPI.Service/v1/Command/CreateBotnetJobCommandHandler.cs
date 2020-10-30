using System.Threading.Tasks;
using System.Threading;
using BotnetJobAPI.Domain.Entities;
using BotnetJobAPI.Data.Repository.v1;
using MediatR;

namespace BotnetJobAPI.Service.v1.Command
{
    public class CreateBotnetJobCommandHandler : IRequestHandler<CreateBotnetJobCommand, BotnetJob>
    {
        private readonly IBotnetJobRepository _botnetJobRepository;

        public CreateBotnetJobCommandHandler(IBotnetJobRepository botnetJobRepository)
        {
            _botnetJobRepository = botnetJobRepository;
        }

        public async Task<BotnetJob> Handle(CreateBotnetJobCommand request, CancellationToken cancellationToken)
        {
            return await _botnetJobRepository.AddAsync(request.BotnetJob);
        }
    }
}
