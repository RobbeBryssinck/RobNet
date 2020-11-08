using System.Threading;
using System.Threading.Tasks;
using Bots.Data.Repository.v1;
using Bots.Domain.Entities;
using MediatR;

namespace Bots.Service.Command
{
    public class DeleteBotCommandHandler : IRequestHandler<DeleteBotCommand, Bot>
    {
        private readonly IBotRepository _botRepository;

        public DeleteBotCommandHandler(IBotRepository botRepository)
        {
            _botRepository = botRepository;
        }

        public async Task<Bot> Handle(DeleteBotCommand request, CancellationToken cancellationToken)
        {
            return await _botRepository.DeleteAsync(request.Bot);
        }
    }
}
