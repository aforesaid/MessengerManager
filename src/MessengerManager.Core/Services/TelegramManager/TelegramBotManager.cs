using System;
using System.Threading.Tasks;
using MessengerManager.Core.Configurations.Telegram;
using MessengerManager.Core.Models.Messengers.Shared;
using MessengerManager.Domain.Entities;
using MessengerManager.Domain.Interfaces;
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
        private readonly IGenericRepository<ChatThreadEntity> _chatThreadRepository;
        private readonly ILogger<TelegramBotManager> _logger;

        public TelegramBotManager(ITelegramBotClient telegramBotClient, 
            ILogger<TelegramBotManager> logger,
            IGenericRepository<ChatThreadEntity> chatThreadRepository, 
            IOptions<TelegramConfiguration> telegramConfiguration)
        {
            _telegramBotClient = telegramBotClient;
            _logger = logger;
            _chatThreadRepository = chatThreadRepository;
            _telegramConfiguration = telegramConfiguration.Value;
        }

        public async Task<int?> SendMessage(ApiMessage message)
        {
            try
            {
                var existThreadDetail = await _chatThreadRepository.GetAll()
                    .FirstOrDefaultAsync(x => x.ThreadName == message.ChatName);
                
                if (existThreadDetail == null)
                {
                    var canMakeThisThread = true;
                    //TODO: добавить список поддерживаемых thread-ов
                    if (canMakeThisThread)
                    {
                        var response = await _telegramBotClient.SendTextMessageAsync(_telegramConfiguration.MainChatId, 
                            message.ChatName);
                        
                        return response.MessageId;
                    }
                    else
                    {
                        _logger.LogWarning("Не могу создать чат с названием {0}", message.ChatName);
                        return null;
                    }
                }
              
                var responseMessage = await _telegramBotClient.SendTextMessageAsync(existThreadDetail.TelegramSupChatId, 
                    message.Text, replyToMessageId: existThreadDetail.MessageId);
                return null;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Не удалось отправить сообщение в канал, message: {0}",
                    message);
                throw;
            }
        }
    }
}