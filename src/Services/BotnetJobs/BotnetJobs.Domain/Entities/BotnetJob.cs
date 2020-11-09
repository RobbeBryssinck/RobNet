using System.Collections.Generic;

namespace BotnetJobs.Domain.Entities
{
    public partial class BotnetJob
    {
        public int Id { get; set; }
        public int BotnetId { get; set; }
        public string JobAction { get; set; }
        public int CommandId { get; set; }
        public string Command { get; set; }
        public string CommandArgument { get; set; }
        public string Status { get; set; }
    }
}
