using BotnetJobs.Domain.Entities;
using MediatR;

namespace BotnetJobs.Service.v1.Command
{
    public class UpdateBotnetJobCommand : IRequest<BotnetJob>
    {
        public BotnetJob BotnetJob { get; set; }
    }
}
