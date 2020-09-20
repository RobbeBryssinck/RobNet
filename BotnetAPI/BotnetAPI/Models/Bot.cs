using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BotnetAPI.Models
{
    public class Bot
    {
        public int Id { get; set; }
        public string IP { get; set; }
        public string Platform { get; set; }
        public string Status { get; set; }
    }
}
