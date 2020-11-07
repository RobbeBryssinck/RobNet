using BotnetJobs.Domain.Entities;
using MediatR;

namespace BotnetJobs.Service.v1.Query
{
    public class GetBotnetJobByBotnetIdQuery : IRequest<BotnetJob>
    {
        public int BotnetId { get; set; }
    }
}
