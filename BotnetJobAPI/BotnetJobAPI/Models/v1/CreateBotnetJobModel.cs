using System.ComponentModel.DataAnnotations;

namespace BotnetJobAPI.Models.v1
{
    public class CreateBotnetJobModel
    {
        [Required]
        public int BotnetId { get; set; }

        [Required]
        public int CommandId { get; set; }
    }
}
