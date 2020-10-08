namespace BotnetAPI.Models
{
    public class Bot
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string IP { get; set; }
        public string Platform { get; set; }
        public string Status { get; set; }
        public string SSHName { get; set; }
    }
}
