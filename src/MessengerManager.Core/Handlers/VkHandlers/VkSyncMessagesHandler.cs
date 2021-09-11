using System;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using MessengerManager.Core.Models.Messengers.Vk;
using MessengerManager.Core.Services.VkManager;
using MessengerManager.Domain.Entities;
using MessengerManager.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace MessengerManager.Core.Handlers.VkHandlers
{
    /// <summary>
    /// Синхронизация сообщений вк с БД
    /// </summary>
    public class VkSyncMessagesHandler : IDisposable
    {
        private readonly ILogger<VkSyncMessagesHandler> _logger;
        private readonly IGenericRepository<ChatThreadEntity> _chatsRepo;
        private readonly IGenericRepository<MessageEntity> _messagesRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IVkBotManager _vkBotManager;

        private Timer _timer;
        private const int TimerTime = 60 * 1000 * 1;
        public VkSyncMessagesHandler(ILogger<VkSyncMessagesHandler> logger, 
            IGenericRepository<MessageEntity> messagesRepo,
            IUnitOfWork unitOfWork,
            IVkBotManager vkBotManager, IGenericRepository<ChatThreadEntity> chatsRepo)
        {
            _logger = logger;
            _messagesRepo = messagesRepo;
            _unitOfWork = unitOfWork;
            _vkBotManager = vkBotManager;
            _chatsRepo = chatsRepo;
        }
        public void SetTimer()
        {
            _timer = new Timer(TimerTime);
            _timer.Elapsed += (_, _) => SyncMessages().Wait();

            _timer.AutoReset = true;
            _timer.Enabled = true;
        }
        public async Task SyncMessages()
        {
            try
            {
                _logger.LogInformation("Начинаю синхронизацию сообщений с Vk");

                var existChats = _chatsRepo.GetAll()
                    .Where(x => x.VkPeerId != default)
                    .ToArray();

                foreach (var chat in existChats)
                {

                    var messages = await _vkBotManager.GetMessages(chat.VkPeerId);
                    foreach (var message in messages.Where(IsValidMessage))
                    {
                        var existMessage = _messagesRepo.GetAll()
                            .FirstOrDefault(x => x.MessageId == message.MessageId);
                        if (existMessage == null)
                        {
                            var newMessage = new MessageEntity(message.UserId.Value, message.Title, message.Date.Value,
                                message.VkPeerId, message.MessageId.Value);
                            _messagesRepo.Add(newMessage);
                            await _unitOfWork.SaveChangesAsync();
                        }
                        //TODO: продумать момент с обновлением сообщений
                    }
                    
                }
                _logger.LogInformation("Синхронизация сообщений с БД успешно завершена");

            }
            catch (Exception e)
            {
                _logger.LogError(e, "Не удалось синхронизировать сообщения с БД");
            }
        }

        private static bool IsValidMessage(ApiVkMessage message)
        {
            return message.Date.HasValue &&
                   message.MessageId.HasValue &&
                   message.UserId.HasValue &&
                   !string.IsNullOrWhiteSpace(message.Title);
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}