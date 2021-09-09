using System.Threading.Tasks;
using MessengerManager.Core.Models.Messengers.Vk;

namespace MessengerManager.Core.Services.VkManager
{
    public interface IVkBotManager
    {
        /// <summary>
        /// Получение всех чатов Вк и информации по ним
        /// </summary>
        /// <param name="withFullDetails"></param>
        /// <returns></returns>
        Task<ApiVkChat[]> GetAllChats();

        /// <summary>
        /// Получение сообщений из чата
        /// </summary>
        /// <param name="vkPeerId"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        Task<ApiVkMessage[]> GetMessages(long vkPeerId, int count = 200);
        /// <summary>
        /// Получение всех пользователей, связанных с главным аккаунтом
        /// </summary>
        /// <returns></returns>
        Task<ApiVkUser[]> GetAllUsers();



        //TODO: читать все чаты, фильтровать только нужные
        //TODO: все сообщения по фильтрованным чатам переносить в Telegram
    }
}