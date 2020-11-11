using System.ComponentModel.DataAnnotations;

namespace Bots.API.Models.v1
{
    public class UpdateBotModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Ip { get; set; }

        [Required]
        public string Platform { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public int BotnetId { get; set; }
    }
}
