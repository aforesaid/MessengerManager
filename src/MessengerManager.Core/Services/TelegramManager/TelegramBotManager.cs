using System;
using System.Threading.Tasks;
using MessengerManager.Core.Configurations.Telegram;
using MessengerManager.Core.Models.Messengers.Telegram;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Telegram.Bot;

namespace MessengerManager.Core.Services.TelegramManager
{
    public class TelegramBotManager : ITelegramBotManager
    {
        private readonly ITelegramBotClient _telegramBotClient;
        private readonly TelegramConfiguration _telegramConfiguration;
        private readonly ILogger<TelegramBotManager> _logger;

        public TelegramBotManager(ITelegramBotClient telegramBotClient, 
            ILogger<TelegramBotManager> logger, 
            IOptions<TelegramConfiguration> telegramConfiguration)
        {
            _telegramBotClient = telegramBotClient;
            _logger = logger;
            _telegramConfiguration = telegramConfiguration.Value;
        }

        public async Task<int> MakeChat(ApiTelegramMakeChat telegramMakeChat)
        {
            try
            {
                var response = await _telegramBotClient.SendTextMessageAsync(_telegramConfiguration.MainChatId, 
                    telegramMakeChat.ChatName);
                            
                return response.MessageId;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Не удалось создать чат с названием {0}", telegramMakeChat.ChatName);
                throw;
            }
        }

        public async Task<int> SendMessageInSupportChat(ApiTelegramSendMessage telegramSendMessage)
        {
            try
            {
                var response = await _telegramBotClient.SendTextMessageAsync(_telegramConfiguration.SupportChatId, 
                    telegramSendMessage.ToString(), replyToMessageId: telegramSendMessage.MessageId);
                            
                return response.MessageId;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Не удалось отправить сообщение текстом {0} к сообщению {1}", 
                    telegramSendMessage.ToString(), telegramSendMessage.MessageId);
                throw;
            }
        }
    }
}