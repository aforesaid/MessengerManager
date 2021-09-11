using System;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using MessengerManager.Core.Models.Messengers.Telegram;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITelegramBotManager _telegramBotManager;
        private readonly IVkBotManager _vkBotManager;

        private Timer _timer;
        private const int TimerTime = 60 * 1000 * 10;

        public VkSyncChatThreadsHandler(ILogger<VkSyncChatThreadsHandler> logger,
            IGenericRepository<ChatThreadEntity> chatRepo,
            ITelegramBotManager telegramBotManager,
            IVkBotManager vkBotManager, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _chatRepo = chatRepo;
            _telegramBotManager = telegramBotManager;
            _vkBotManager = vkBotManager;
            _unitOfWork = unitOfWork;
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
                const int limitCount = 10;
                var currentItems = 0;
                var chats = await _vkBotManager.GetAllChats();
                foreach (var chat in chats)
                {
                    if (currentItems > limitCount)
                        break;
                    
                    var existChat = _chatRepo.GetAll()
                        .FirstOrDefault(x => x.ThreadName == chat.ChatThreadName);
                    if (existChat == null)
                    {
                        await MakeChatThread(chat);
                        currentItems++;
                    }
                    else
                    {
                        existChat.UpdateVkPeerId(chat.VkPeerId);
                        _chatRepo.Update(existChat);

                        await _unitOfWork.SaveChangesAsync();
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
            await Task.Delay(new Random().Next(1500, 2500));
            var request = new ApiTelegramMakeChat(chat.ChatThreadName);
            await _telegramBotManager.MakeChat(request);
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}