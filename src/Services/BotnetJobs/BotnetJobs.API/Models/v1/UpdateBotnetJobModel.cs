using System;
using System.ComponentModel.DataAnnotations;

namespace BotnetJobs.API.Models.v1
{
    public class UpdateBotnetJobModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int BotnetId { get; set; }

        [Required]
        public int CommandId { get; set; }

        [Required]
        public string Status { get; set; }
    }
}
