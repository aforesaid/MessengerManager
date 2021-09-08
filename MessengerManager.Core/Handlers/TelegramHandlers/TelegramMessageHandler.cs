using System;
using System.Threading;
using MessengerManager.Domain.Entities;
using MessengerManager.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace MessengerManager.Core.Handlers.TelegramHandlers
{
    public class TelegramMessageHandler
    {
        private readonly IGenericRepository<ChatThreadEntity> _chatThreadRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<TelegramMessageHandler> _logger;
        public TelegramMessageHandler(IGenericRepository<ChatThreadEntity> chatThreadRepository, 
            IUnitOfWork unitOfWork,
            ILogger<TelegramMessageHandler> logger)
        {
            _chatThreadRepository = chatThreadRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
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
                //Создание нового чата
                var chatThreadName = update.Message.Text;
                var existChatThread = await _chatThreadRepository.GetAll()
                    .FirstOrDefaultAsync(x => x.ThreadName == chatThreadName, cancellationToken: cancellationToken);
                
                
                if (existChatThread != null)
                {
                    _logger.LogWarning("{0}: ChatThread {1} уже существует!", 
                        nameof(TelegramMessageHandler), chatThreadName);
                    return;
                }

                var newThread = new ChatThreadEntity(update.Message.Chat.Id.ToString(), update.Message.Text,
                    update.Message.MessageId);
                
                _chatThreadRepository.Add(newThread);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
        }
    }
}