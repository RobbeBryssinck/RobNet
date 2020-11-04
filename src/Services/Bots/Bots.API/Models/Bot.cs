using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bots.API.Models
{
    public class Bot
    {
        [Key]
        public int Id { get; set; }
        public string IP { get; set; }
        public string SSHName { get; set; }
        public string Status { get; set; }

        public Botnet Botnet { get; set; }
        public int BotnetId { get; set; }
    }
}
