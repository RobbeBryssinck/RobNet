using System.Threading;
using System.Threading.Tasks;
using BotnetJobs.Data.Repository.v1;
using BotnetJobs.Domain.Entities;
using MediatR;

namespace BotnetJobs.Service.v1.Command
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
