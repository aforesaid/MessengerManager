using System.Linq;
using System.Threading.Tasks;
using MessengerManager.Core.Configurations.Vk;
using MessengerManager.Core.Services.VkManager;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using VkNet;
using VkNet.AudioBypassService.Extensions;
using VkNet.Model;
using Xunit;

namespace MessengerManager.Tests.UnitTests.Services.VkManager
{
    public class VkManagerTest
    {
        private readonly IVkBotManager _vkBotManager;
        public VkManagerTest()
        {
            var config = new ConfigurationBuilder()
                .AddUserSecrets<VkManagerTest>()
                .Build(); 
            var vkConfiguration = config.GetSection(nameof(VkConfiguration))
                .Get<VkConfiguration>();

            var serviceCollection = new ServiceCollection();
            serviceCollection.AddAudioBypass();
            var vkApi = new VkApi(serviceCollection);
            vkApi.Authorize(new ApiAuthParams
            {
                Login = vkConfiguration.Login,
                Password = vkConfiguration.Password
            });

            var mockLogger = new Mock<ILogger<VkBotManager>>();
            _vkBotManager = new VkBotManager(vkApi, mockLogger.Object, Options.Create(vkConfiguration));
        }

        [Fact]
        public async Task GetAllChats()
        {
            var chats = await _vkBotManager.GetAllChats();
            
            Assert.NotEmpty(chats);
            Assert.NotNull(chats.First().ChatThreadName);
        }

        [Fact]
        public async Task GetMessages()
        {
            var chats = await _vkBotManager.GetAllChats();

            var vkPeerId = chats.First().VkPeerId;
            var messages = await _vkBotManager.GetMessages(vkPeerId);
            
            Assert.NotEmpty(messages);
            
            Assert.NotNull(messages.First().Date);
            Assert.NotNull(messages.First().Title);
            Assert.NotNull(messages.First().MessageId);
            Assert.NotNull(messages.First().UserId);
        }
    }
}