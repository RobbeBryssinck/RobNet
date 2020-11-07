using Bots.Domain.Entities;
using MediatR;

namespace Bots.Service.Command
{
    public class UpdateBotnetCommand : IRequest
    {
        public Botnet Botnet { get; set; }
    }
}
