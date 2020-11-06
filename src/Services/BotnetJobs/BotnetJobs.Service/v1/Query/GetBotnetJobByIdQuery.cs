using System;
using BotnetJobs.Domain.Entities;
using MediatR;

namespace BotnetJobs.Service.v1.Query
{
    public class GetBotnetJobByIdQuery : IRequest<BotnetJob>
    {
        public int Id { get; set; }
    }
}
