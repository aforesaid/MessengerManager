using System;
using System.Linq;
using System.Threading.Tasks;
using MessengerManager.Core.Models.Messengers.Shared;
using MessengerManager.Core.Models.Messengers.Vk;
using MessengerManager.Core.Services.TelegramManager;
using MessengerManager.Core.Services.VkManager;
using MessengerManager.Domain.Entities;
using MessengerManager.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace MessengerManager.Core.Handlers.VkHandlers
{
    /// <summary>
    /// Синхронизация чатов с Tg
    /// </summary>
    public class VkSyncChatThreadsHandler
    {
        private readonly ILogger<VkSyncChatThreadsHandler> _logger;
        private readonly IGenericRepository<ChatThreadEntity> _chatRepo;
        private readonly ITelegramBotManager _telegramBotManager;
        private readonly IVkBotManager _vkBotManager;

        public VkSyncChatThreadsHandler(ILogger<VkSyncChatThreadsHandler> logger,
            IGenericRepository<ChatThreadEntity> chatRepo,
            ITelegramBotManager telegramBotManager,
            IVkBotManager vkBotManager)
        {
            _logger = logger;
            _chatRepo = chatRepo;
            _telegramBotManager = telegramBotManager;
            _vkBotManager = vkBotManager;
        }

        public async Task SyncChats()
        {
            try
            {
                var chats = await _vkBotManager.GetAllChats();
                foreach (var chat in chats)
                {
                    var existChat = _chatRepo.GetAll()
                        .FirstOrDefault(x => x.ThreadName == chat.ChatThreadName);
                    if (existChat == null)
                    {
                        await MakeChatThread(chat);
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Не удалось синхронизировать чаты с Тг");
                throw;
            }
        }

        private async Task MakeChatThread(ApiVkChat chat)
        {
            var request = new ApiTelegramMessage(null, null, chat.ChatThreadName, DateTime.Now);
            await _telegramBotManager.SendMessage(request);
        }
    }
}