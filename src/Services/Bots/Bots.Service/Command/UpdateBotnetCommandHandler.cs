using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Bots.Data.Repository.v1;
using MediatR;

namespace Bots.Service.Command
{
    public class UpdateBotnetCommandHandler : IRequestHandler<UpdateBotnetCommand>
    {
        private readonly IBotnetRepository _botnetRepository;

        public UpdateBotnetCommandHandler(IBotnetRepository botnetRepository)
        {
            _botnetRepository = botnetRepository;
        }

        public async Task<Unit> Handle(UpdateBotnetCommand request, CancellationToken cancellationToken)
        {
            await _botnetRepository.UpdateAsync(request.Botnet);

            return Unit.Value;
        }
    }
}
