using System.Threading;
using System.Threading.Tasks;
using BotnetJobs.Data.Repository.v1;
using BotnetJobs.Domain.Entities;
using BotnetJobs.Messaging.Send.Sender.v1;
using MediatR;

namespace BotnetJobs.Service.v1.Command
{
    public class CreateBotnetJobCommandHandler : IRequestHandler<CreateBotnetJobCommand, BotnetJob>
    {
        private readonly IBotnetJobRepository _botnetJobRepository;
        private readonly IBotnetJobUpdateSender _botnetJobUpdateSender;

        public CreateBotnetJobCommandHandler(IBotnetJobRepository botnetJobRepository, IBotnetJobUpdateSender botnetJobUpdateSender)
        {
            _botnetJobRepository = botnetJobRepository;
            _botnetJobUpdateSender = botnetJobUpdateSender;
        }

        public async Task<BotnetJob> Handle(CreateBotnetJobCommand request, CancellationToken cancellationToken)
        {
            var botnetJob = await _botnetJobRepository.AddAsync(request.BotnetJob);

            _botnetJobUpdateSender.SendBotnetJob(botnetJob);

            return botnetJob;
        }
    }
}
