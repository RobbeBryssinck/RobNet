using BotnetAPI;

namespace BotnetController.Logic
{
    public class ServerManager
    {
        public StartJobResponse StartJob(StartJobRequest request)
        {
            return new StartJobResponse();
        }

        private C2Server GetC2Server(int userId)
        {
            return new C2Server();
        }
    }
}
