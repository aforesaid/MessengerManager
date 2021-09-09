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

        //TODO: читать все чаты, фильтровать только нужные
        //TODO: все сообщения по фильтрованным чатам переносить в Telegram
    }
}