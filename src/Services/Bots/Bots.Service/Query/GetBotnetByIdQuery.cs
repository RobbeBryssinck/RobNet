using Bots.Domain.Entities;
using MediatR;

namespace Bots.Service.Query
{
    public class GetBotnetByIdQuery : IRequest<Botnet>
    {
        public int Id { get; set; }
    }
}
