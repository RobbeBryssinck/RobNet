using BotnetJobAPI.Domain.Entities;
using MediatR;

namespace BotnetJobAPI.Service.v1.Command
{
    public class UpdateBotnetJobCommand : IRequest<BotnetJob>
    {
        public BotnetJob BotnetJob { get; set; }
    }
}
