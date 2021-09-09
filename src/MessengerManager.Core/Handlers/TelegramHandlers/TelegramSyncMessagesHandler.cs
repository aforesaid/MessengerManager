using System;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using MessengerManager.Core.Models.Messengers.Shared;
using MessengerManager.Core.Services.TelegramManager;
using MessengerManager.Domain.Entities;
using MessengerManager.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace MessengerManager.Core.Handlers.TelegramHandlers
{
    /// <summary>
    /// Синхронизация сообщений из БД с Тг
    /// </summary>
    public class TelegramSyncMessagesHandler
    {
        private readonly ILogger<TelegramSyncMessagesHandler> _logger;
        private readonly IGenericRepository<ChatThreadEntity> _chatsRepo;
        private readonly IGenericRepository<MessageEntity> _messagesRepo;
        private readonly IGenericRepository<UserEntity> _usersRepo;
        private readonly ITelegramBotManager _telegramBotManager;
        private readonly IUnitOfWork _unitOfWork;
        
        private Timer _timer;
        private const int TimerTime = 60 * 1000 * 10;

        public TelegramSyncMessagesHandler(ILogger<TelegramSyncMessagesHandler> logger,
            IGenericRepository<ChatThreadEntity> chatsRepo, 
            IGenericRepository<MessageEntity> messagesRepo,
            IGenericRepository<UserEntity> usersRepo, 
            ITelegramBotManager telegramBotManager,
            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _chatsRepo = chatsRepo;
            _messagesRepo = messagesRepo;
            _usersRepo = usersRepo;
            _telegramBotManager = telegramBotManager;
            _unitOfWork = unitOfWork;
        }
        public void SetTimer()
        {
            _timer = new Timer(TimerTime);
            _timer.Elapsed += (_, _) => SyncMessagesWithTelegram().Wait();

            _timer.AutoReset = true;
            _timer.Enabled = true;
        }

        public async Task SyncMessagesWithTelegram()
        {
            try
            {
                _logger.LogInformation("Начинаю синхронизацию с Tg");

                var chats = _chatsRepo.GetAll()
                    .Where(x => x.VkPeerId != default);
                var messages = _messagesRepo.GetAll()
                    .Where(x => !x.Sent && x.UserId != default && x.VkPeerId != default);
                
                var q = from chat in chats
                    from message in messages.Where(x => x.VkPeerId == chat.VkPeerId)
                    from user in _usersRepo.GetAll().DefaultIfEmpty()
                    select new {chat, message, user};
                foreach (var item in q)
                {
                    var from = item.user.ToString();
                    var text = item.message.Text;
                    var date = item.message.Date;
                    var chatName = item.chat.ThreadName;

                    var request = new ApiTelegramMessage(from, text, chatName, date);
                    var response = await _telegramBotManager.SendMessage(request);

                    if (response.HasValue)
                    {
                        item.message.SetSent();
                        _messagesRepo.Update(item.message);
                        await  _unitOfWork.SaveChangesAsync();
                    }
                }
                
                _logger.LogInformation("Синхронизация сообщений с Tg успешно завершена");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Не удалось отправить все необходимые сообщения в Tg");
                throw;
            }
        }
    }
}