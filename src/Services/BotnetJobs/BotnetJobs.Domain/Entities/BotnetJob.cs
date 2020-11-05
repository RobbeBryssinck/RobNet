using System;

namespace BotnetJobs.Domain.Entities
{
    public partial class BotnetJob
    {
        public Guid Id { get; set; }
        public int BotnetId { get; set; }
        public int CommandId { get; set; }
    }
}
