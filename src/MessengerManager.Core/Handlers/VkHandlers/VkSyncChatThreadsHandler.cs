using System;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
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
    public class VkSyncChatThreadsHandler : IDisposable
    {
        private readonly ILogger<VkSyncChatThreadsHandler> _logger;
        private readonly IGenericRepository<ChatThreadEntity> _chatRepo;
        private readonly ITelegramBotManager _telegramBotManager;
        private readonly IVkBotManager _vkBotManager;

        private Timer _timer;
        private const int TimerTime = 60 * 1000 * 10;

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

        public void SetTimer()
        {
            _timer = new Timer(TimerTime);
            _timer.Elapsed += (_, _) => SyncChats().Wait();

            _timer.AutoReset = true;
            _timer.Enabled = true;
        }

        public async Task SyncChats()
        {
            try
            {
                _logger.LogInformation("Начинаю синхронизацию чатов с Vk");

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
                _logger.LogInformation("Синхронизация чатов успешно завершена");

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

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}