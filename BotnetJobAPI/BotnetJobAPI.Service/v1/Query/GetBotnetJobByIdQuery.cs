using System;
using BotnetJobAPI.Domain.Entities;
using MediatR;

namespace BotnetJobAPI.Service.v1.Query
{
    public class GetBotnetJobByIdQuery : IRequest<BotnetJob>
    {
        public Guid Id { get; set; }
    }
}
