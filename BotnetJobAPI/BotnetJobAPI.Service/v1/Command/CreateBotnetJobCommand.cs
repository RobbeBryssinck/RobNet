using BotnetJobAPI.Domain.Entities;
using MediatR;

namespace BotnetJobAPI.Service.v1.Command
{
    public class CreateBotnetJobCommand : IRequest<BotnetJob>
    {
        public BotnetJob BotnetJob { get; set; }
    }
}
