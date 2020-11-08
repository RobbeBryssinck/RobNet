using System.Linq;
using Bots.Domain.Entities;

namespace Bots.Data.Database
{
    public class DbInitializer
    {
        public static void Initialize(BotsContext context)
        {
            context.Database.EnsureCreated();

            if (context.Botnets.Any())
                return;

            var botnets = new Botnet[]
            {
                new Botnet
                {
                    Status = "Waiting",
                },
                new Botnet
                {
                    Status = "Waiting",
                },
                new Botnet
                {
                    Status = "Working",
                },
            };

            foreach (Botnet botnet in botnets)
            {
                context.Botnets.Add(botnet);
            }

            var bots = new Bot[]
            {
                new Bot
                {
                    BotnetId = 1,
                    IP = "253.5.74.55",
                    Platform = "Windows",
                    SSHName = "Hank",
                    Status = "Offline",
                },
                new Bot
                {
                    BotnetId = 1,
                    IP = "55.124.22.231",
                    Platform = "Linux",
                    SSHName = "Rachel",
                    Status = "Working",
                },
                new Bot
                {
                    BotnetId = 1,
                    IP = "55.78.92.229",
                    Platform = "Windows",
                    SSHName = "Fred",
                    Status = "Working",
                },
                new Bot
                {
                    BotnetId = 2,
                    IP = "52.112.45.62",
                    Platform = "Linux",
                    SSHName = "Peter",
                    Status = "Waiting",
                },
                new Bot
                {
                    BotnetId = 2,
                    IP = "82.33.34.239",
                    Platform = "Linux",
                    SSHName = "Denise",
                    Status = "Offline",
                },
                new Bot
                {
                    BotnetId = 2,
                    IP = "52.78.63.197",
                    Platform = "Windows",
                    SSHName = "Natalie",
                    Status = "Waiting",
                },
                new Bot
                {
                    BotnetId = 3,
                    IP = "96.128.22.47",
                    Platform = "Windows",
                    SSHName = "Rob",
                    Status = "Waiting",
                },
                new Bot
                {
                    BotnetId = 3,
                    IP = "71.114.223.84",
                    Platform = "Linux",
                    SSHName = "Tom",
                    Status = "Waiting",
                },
                new Bot
                {
                    BotnetId = 3,
                    IP = "34.227.32.68",
                    Platform = "Windows",
                    SSHName = "Eliza",
                    Status = "Offline",
                },
                new Bot
                {
                    BotnetId = 3,
                    IP = "88.123.22.293",
                    Platform = "Linux",
                    SSHName = "Richard",
                    Status = "Waiting",
                },
            };

            foreach (Bot bot in bots)
            {
                context.Bots.Add(bot);
            }

            context.SaveChanges();
        }
    }
}
