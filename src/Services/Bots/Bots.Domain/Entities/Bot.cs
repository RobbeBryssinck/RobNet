namespace Bots.Domain.Entities
{
    public class Bot
    {
        public int Id { get; set; }
        public string IP { get; set; }
        public string SSHName { get; set; }
        public string Status { get; set; }

        public Botnet Botnet { get; set; }
        public int BotnetId { get; set; }
    }
}
