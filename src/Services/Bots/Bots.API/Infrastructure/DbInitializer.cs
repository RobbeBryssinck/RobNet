using Bots.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bots.API.Infrastructure
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
                    Status = "Working",
                },
                new Botnet
                {
                    Status = "Waiting",
                },
                new Botnet
                {
                    Status = "Waiting",
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
                    SSHName = "Hank",
                    Status = "Offline",
                },
                new Bot
                {
                    BotnetId = 1,
                    IP = "55.124.22.231",
                    SSHName = "Rachel",
                    Status = "Working",
                },
                new Bot
                {
                    BotnetId = 1,
                    IP = "55.78.92.229",
                    SSHName = "Fred",
                    Status = "Working",
                },
                new Bot
                {
                    BotnetId = 2,
                    IP = "52.112.45.62",
                    SSHName = "Peter",
                    Status = "Waiting",
                },
                new Bot
                {
                    BotnetId = 2,
                    IP = "82.33.34.239",
                    SSHName = "Denise",
                    Status = "Offline",
                },
                new Bot
                {
                    BotnetId = 2,
                    IP = "52.78.63.197",
                    SSHName = "Natalie",
                    Status = "Waiting",
                },
                new Bot
                {
                    BotnetId = 3,
                    IP = "96.128.22.47",
                    SSHName = "Rob",
                    Status = "Waiting",
                },
                new Bot
                {
                    BotnetId = 3,
                    IP = "71.114.223.84",
                    SSHName = "Tom",
                    Status = "Waiting",
                },
                new Bot
                {
                    BotnetId = 3,
                    IP = "34.227.32.68",
                    SSHName = "Eliza",
                    Status = "Offline",
                },
                new Bot
                {
                    BotnetId = 3,
                    IP = "88.123.22.293",
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
