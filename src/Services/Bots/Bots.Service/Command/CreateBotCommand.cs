using Bots.Domain.Entities;
using MediatR;

namespace Bots.Service.Command
{
    public class CreateBotCommand : IRequest<Bot>
    {
        public Bot Bot { get; set; }
    }
}
