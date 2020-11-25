using System.ComponentModel.DataAnnotations;

namespace Bots.API.Models.v1
{
    public class CreateBotModel
    {
        [Required]
        public string IP { get; set; }

        [Required]
        public string Platform { get; set; }

        public string Status { get; set; }

        [Required]
        public int BotnetId { get; set; }
    }
}
