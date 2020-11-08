namespace Bots.Service.Models
{
    public class UpdateBotnetStatusModel
    {
        public int BotnetId { get; set; }
        public string Status { get; set; }
        public string Command { get; set; }
    }
}
