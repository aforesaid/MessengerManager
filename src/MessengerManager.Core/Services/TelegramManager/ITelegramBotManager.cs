using System.Threading.Tasks;
using MessengerManager.Core.Models.Messengers.Shared;

namespace MessengerManager.Core.Services.TelegramManager
{
    public interface ITelegramBotManager
    {
        /// <summary>
        /// Отправка сообщения в тг
        /// </summary>
        /// <param name="telegramMessage"></param>
        /// <returns>Null - не было отправлено в тред, вероятно был создан новый тред</returns>
        Task<int?> SendMessage(ApiTelegramMessage telegramMessage);
    }
}