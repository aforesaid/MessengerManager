using Microsoft.Extensions.Logging;
using Telegram.Bot;

namespace MessengerManager.Core.Services.TelegramManager
{
    public class TelegramManager : ITelegramManager
    {
        private readonly ITelegramBotClient _telegramBotClient;
        private readonly ILogger<TelegramManager> _logger;

        public TelegramManager(ITelegramBotClient telegramBotClient, 
            ILogger<TelegramManager> logger)
        {
            _telegramBotClient = telegramBotClient;
            _logger = logger;
        }
    }
}