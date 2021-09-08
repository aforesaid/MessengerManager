using System;
using System.Threading;
using System.Threading.Tasks;
using MessengerManager.Core.Models.Messengers.Shared;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace MessengerManager.Core.Services.TelegramManager
{
    public class TelegramBotManager : ITelegramBotManager
    {
        private readonly ITelegramBotClient _telegramBotClient;
        private readonly ILogger<TelegramBotManager> _logger;

        public TelegramBotManager(ITelegramBotClient telegramBotClient, 
            ILogger<TelegramBotManager> logger)
        {
            _telegramBotClient = telegramBotClient;
            _logger = logger;
        }

        public async Task<int> SendMessage(ApiTelegramMessage telegramMessage)
        {
            try
            {
                var response = await _telegramBotClient.SendTextMessageAsync(telegramMessage.ChatId, 
                    telegramMessage.Text, 
                    replyToMessageId: telegramMessage.ReplyMessageId);
                return response.MessageId;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Не удалось отправить сообщение в канал, message: {0}",
                    telegramMessage);
                throw;
            }
        }
    }
}