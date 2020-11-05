using BotnetJobs.Domain.Entities;

namespace BotnetJobs.Messaging.Send.Sender.v1
{
    public interface IBotnetJobUpdateSender
    {
        void SendBotnetJob(BotnetJob botnetJob);
    }
}
