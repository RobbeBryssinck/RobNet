using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Bots.Data.Repository.v1;
using Bots.Domain.Entities;
using MediatR;

namespace Bots.Service.Command
{
    public class CreateBotCommandHandler : IRequestHandler<CreateBotCommand, Bot>
    {
        private readonly IBotRepository _botRepository;

        public CreateBotCommandHandler(IBotRepository botRepository)
        {
            _botRepository = botRepository;
        }

        public async Task<Bot> Handle(CreateBotCommand request, CancellationToken cancellationToken)
        {
            return await _botRepository.AddAsync(request.Bot);
        }
    }
}
