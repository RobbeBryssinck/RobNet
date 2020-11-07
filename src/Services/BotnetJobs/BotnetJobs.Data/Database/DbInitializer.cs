using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Internal;

namespace BotnetJobs.Data.Database
{
    public class DbInitializer
    {
        public static void Initialize(BotnetJobContext context)
        {
            context.Database.EnsureCreated();

            if (context.BotnetJobs.Any())
                return;

            // Potential seeder code
        }
    }
}
