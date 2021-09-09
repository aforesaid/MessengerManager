using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using VkNet;
using VkNet.Abstractions;
using VkNet.Enums.Filters;
using VkNet.Model;

namespace MessengerManager.Core.Services.VkManager
{
    public class VkBotManager : IVkBotManager
    {
        private readonly IVkApi _vkApi;
        private readonly ILogger<VkBotManager> _logger;
        public async Task Test()
        {
        }
    }
}