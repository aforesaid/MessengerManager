using System.Threading.Tasks;
using MessengerManager.Core.Models.Messengers.Shared;

namespace MessengerManager.Core.Services.TelegramManager
{
    public interface ITelegramBotManager
    {
        /// <summary>
        /// Null - не дефолтное сообщение
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        Task<int?> SendMessage(ApiMessage message);
    }
}