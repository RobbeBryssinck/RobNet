using Bots.Domain.Entities;
using MediatR;

namespace Bots.Service.Query
{
    public class GetBotByIdQuery : IRequest<Bot>
    {
        public int Id { get; set; }
    }
}
