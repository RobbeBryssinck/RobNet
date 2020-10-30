using BotnetJobAPI.Domain.Entities;

namespace BotnetJobAPI.Messaging.Send.Sender.v1
{
    public interface IBotnetJobUpdateSender
    {
        void SendBotnetJob(BotnetJob botnetJob);
    }
}
