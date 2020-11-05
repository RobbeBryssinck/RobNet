using System;
using System.ComponentModel.DataAnnotations;

namespace BotnetJobs.API.Models.v1
{
    public class UpdateBotnetJobModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public int BotnetId { get; set; }

        [Required]
        public int CommandId { get; set; }
    }
}
