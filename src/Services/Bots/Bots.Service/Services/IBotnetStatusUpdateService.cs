using Bots.Service.Models;

namespace Bots.Service.Services
{
    public interface IBotnetStatusUpdateService
    {
        void UpdateBotnetStatus(UpdateBotnetStatusModel updateBotnetStatusModel);
    }
}
