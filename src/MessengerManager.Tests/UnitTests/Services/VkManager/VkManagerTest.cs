using System.Threading.Tasks;
using MessengerManager.Core.Services.VkManager;
using Xunit;

namespace MessengerManager.Tests.UnitTests.Services.VkManager
{
    public class VkManagerTest
    {
        private readonly IVkBotManager _vkBotManager;
        public VkManagerTest()
        {
            _vkBotManager = new VkBotManager();
        }

        [Fact]
        public async Task Test()
        {
            await _vkBotManager.Test();
        }
    }
}