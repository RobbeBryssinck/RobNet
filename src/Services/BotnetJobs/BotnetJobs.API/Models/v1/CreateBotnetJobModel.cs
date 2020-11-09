using System.ComponentModel.DataAnnotations;

namespace BotnetJobs.API.Models.v1
{
    public class CreateBotnetJobModel
    {
        [Required]
        public int BotnetId { get; set; }

        [Required]
        public int CommandId { get; set; }

        [Required]
        public string Command { get; set; }

        public string CommandArgument { get; set; }
    }
}
