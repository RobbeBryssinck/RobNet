using BotnetController.Logic;
using System.Collections.Generic;

namespace BotnetController.Data
{
    public class C2Job
    {
        public int Id { get; set; }
        public int BotnetId { get; set; }
        public string Status { get; set; }
    }
}
