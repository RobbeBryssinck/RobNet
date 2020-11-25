﻿using Bots.Data.Database;
using Bots.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bots.IntegrationTests
{
    public static class Utilities
    {
        public static void InitializeDbForTests(BotsContext context)
        {
            context.Bots.AddRange(GetSeedingBots());
            context.SaveChanges();
        }

        public static void ReinitializeDbForTests(BotsContext context)
        {
            context.Bots.RemoveRange(context.Bots);
            InitializeDbForTests(context);
        }

        private static Bot[] GetSeedingBots()
        {
            return new Bot[]
            {
                //new Bot
                //{
                //    BotnetId = 1,
                //    IP = "192.168.175.129",
                //    Platform = "Linux",
                //    Status = "Waiting",
                //},
                new Bot
                {
                    BotnetId = 1,
                    IP = "192.168.175.135",
                    Platform = "Linux",
                    Status = "Waiting",
                },
                new Bot
                {
                    BotnetId = 1,
                    IP = "55.78.92.229",
                    Platform = "Windows",
                    Status = "Waiting",
                },
                new Bot
                {
                    BotnetId = 2,
                    IP = "52.112.45.62",
                    Platform = "Linux",
                    Status = "Waiting",
                },
                new Bot
                {
                    BotnetId = 2,
                    IP = "82.33.34.239",
                    Platform = "Linux",
                    Status = "Offline",
                },
                new Bot
                {
                    BotnetId = 2,
                    IP = "52.78.63.197",
                    Platform = "Windows",
                    Status = "Waiting",
                },
                new Bot
                {
                    BotnetId = 3,
                    IP = "96.128.22.47",
                    Platform = "Windows",
                    Status = "Waiting",
                },
                new Bot
                {
                    BotnetId = 3,
                    IP = "71.114.223.84",
                    Platform = "Linux",
                    Status = "Waiting",
                },
                new Bot
                {
                    BotnetId = 3,
                    IP = "34.227.32.68",
                    Platform = "Windows",
                    Status = "Offline",
                },
                new Bot
                {
                    BotnetId = 3,
                    IP = "88.123.22.293",
                    Platform = "Linux",
                    Status = "Waiting",
                },
            };
        }
    }
}
