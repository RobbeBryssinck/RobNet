using System.Threading;
using System.Threading.Tasks;
using Bots.Data.Repository.v1;
using Bots.Domain.Entities;
using MediatR;

namespace Bots.Service.Command
{
    public class UpdateBotCommandHandler : IRequestHandler<UpdateBotCommand, Bot>
    {
        private readonly IBotRepository _botRepository;

        public UpdateBotCommandHandler(IBotRepository botRepository)
        {
            _botRepository = botRepository;
        }

        public async Task<Bot> Handle(UpdateBotCommand updateBotCommand, CancellationToken cancellationToken)
        {
            return await _botRepository.UpdateAsync(updateBotCommand.Bot);
        }
    }
}
