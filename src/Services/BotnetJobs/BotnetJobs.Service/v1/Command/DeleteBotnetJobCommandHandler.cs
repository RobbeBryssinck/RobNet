using System.Threading;
using System.Threading.Tasks;
using BotnetJobs.Data.Repository.v1;
using BotnetJobs.Domain.Entities;
using BotnetJobs.Messaging.Send.Sender.v1;
using MediatR;

namespace BotnetJobs.Service.v1.Command
{
    public class DeleteBotnetJobCommandHandler : IRequestHandler<DeleteBotnetJobCommand, BotnetJob>
    {
        private readonly IBotnetJobRepository _botnetJobRepository;
        private readonly IBotnetJobUpdateSender _botnetJobUpdateSender;

        public DeleteBotnetJobCommandHandler(IBotnetJobRepository botnetJobRepository, IBotnetJobUpdateSender botnetJobUpdateSender)
        {
            _botnetJobRepository = botnetJobRepository;
            _botnetJobUpdateSender = botnetJobUpdateSender;
        }

        public async Task<BotnetJob> Handle(DeleteBotnetJobCommand request, CancellationToken cancellationToken)
        {
            var botnetJob = await _botnetJobRepository.DeleteAsync(request.BotnetJob);

            _botnetJobUpdateSender.SendBotnetJob(botnetJob);

            return botnetJob;
        }
    }
}
