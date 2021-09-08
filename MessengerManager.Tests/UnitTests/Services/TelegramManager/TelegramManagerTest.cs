using System;
using System.Threading.Tasks;
using MessengerManager.Core.Configurations.Telegram;
using MessengerManager.Core.Handlers.TelegramHandlers;
using MessengerManager.Core.Models.Messengers.Shared;
using MessengerManager.Core.Services.TelegramManager;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace MessengerManager.Tests.UnitTests.Services.TelegramManager
{
    public class TelegramManagerTest
    {
        private readonly ITelegramBotManager _telegramBotManager;
        private readonly ITelegramBotClient _telegramBotClient;
        private readonly TelegramConfiguration _telegramConfiguration;
        public TelegramManagerTest()
        {
            var config = new ConfigurationBuilder()
                .AddUserSecrets<TelegramManagerTest>()
                .Build();            
            
            var mockLogger = new Mock<ILogger<TelegramBotManager>>();
            _telegramConfiguration = new TelegramConfiguration(config["telegramToken"], config["telegramChatId"],
                config["telegramSupportedId"]);
            
            _telegramBotClient = new TelegramBotClient(config["telegramToken"]);
            var option = Options.Create(_telegramConfiguration);
            _telegramBotManager = new TelegramBotManager(_telegramBotClient, mockLogger.Object);
        }

        [Fact]
        public async Task SendMessage()
        {
            await Setup();
            var request = new ApiTelegramMessage("TestMessage#2", _telegramConfiguration.MainChatId);
            var messageId = await _telegramBotManager.SendMessage(request);
            request = new ApiTelegramMessage("ResponseMessage#2", _telegramConfiguration.MainChatId, messageId);
            await _telegramBotManager.SendMessage(request);
            Console.ReadKey();
        }
        private async Task Setup()
        {
            var handler = new TelegramMessageHandler(Options.Create(_telegramConfiguration));
            Task.Run(async () => await BaseTelegramHandler.StartHandler(_telegramBotClient, UpdateType.Message,
                handler.UpdateHandler,
                handler.ErrorHandler));
        }
    }
}