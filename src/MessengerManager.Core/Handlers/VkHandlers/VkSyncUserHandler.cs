using System;
using System.Threading.Tasks;
using System.Timers;
using MessengerManager.Core.Services.VkManager;
using MessengerManager.Domain.Entities;
using MessengerManager.Domain.Interfaces;
using MessengerManager.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MessengerManager.Core.Handlers.VkHandlers
{
    /// <summary>
    /// Синхронизация пользователей с БД
    /// </summary>
    public class VkSyncUserHandler
    {
        private readonly ILogger<VkSyncUserHandler> _logger;
        private readonly EfGenericRepository<UserEntity> _usersRepository;
        private readonly IUnitOfWork _unitOfWork;

        private readonly IVkBotManager _vkBotManager;
        
        private Timer _timer;
        private const int TimerTime = 60 * 1000 * 10;
        public VkSyncUserHandler(ILogger<VkSyncUserHandler> logger, 
            EfGenericRepository<UserEntity> usersRepository, 
            IUnitOfWork unitOfWork, 
            IVkBotManager vkBotManager)
        {
            _logger = logger;
            _usersRepository = usersRepository;
            _unitOfWork = unitOfWork;
            _vkBotManager = vkBotManager;
        }
        public void SetTimer()
        {
            _timer = new Timer(TimerTime);
            _timer.Elapsed += (_, _) => SyncUsers().Wait();

            _timer.AutoReset = true;
            _timer.Enabled = true;
        }

        public async Task SyncUsers()
        {
            try
            {
                var allUsers = await _vkBotManager.GetAllUsers();
                foreach (var user in allUsers)
                {
                    var existUser = await _usersRepository.GetAll()
                        .FirstOrDefaultAsync(x => x.UserId == user.UserId);
                    if (existUser == null)
                    {
                        var newUser = new UserEntity(user.Name, user.LastName, user.UniqueId,
                            user.UserId);
                        _usersRepository.Add(newUser);
                    }
                    else
                    {
                        existUser.UpdateName(user.Name);
                        existUser.UpdateLastName(user.LastName);
                        existUser.UpdateUniqueId(user.UniqueId);

                        _usersRepository.Update(existUser);
                    }

                    await _unitOfWork.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Не удалось синхронизировать пользователей");
            }
        }
    }
}