using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MessengerManager.Core.Configurations.Telegram;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace MessengerManager.Core.Handlers.TelegramHandlers
{
    public class TelegramMessageHandler
    {
        private IOptions<TelegramConfiguration> _config;
        private static readonly List<int> _messages = new();

        public TelegramMessageHandler(IOptions<TelegramConfiguration> config)
        {
            _config = config;
        }

        public async void ErrorHandler(ITelegramBotClient telegramBotClient, Exception exception, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async void UpdateHandler(ITelegramBotClient telegramBotClient, Update update,
            CancellationToken cancellationToken)
        {
            const int telegramId = 777000;
            
            if (update.Message?.From?.Id == telegramId && update.Message.ReplyToMessage == null)
            {
                //Новый чат
            }
        }
    }
}