using System.Threading.Tasks;
using MessengerManager.Core.Models.Messengers.Telegram;

namespace MessengerManager.Core.Services.TelegramManager
{
    public interface ITelegramBotManager
    {
        /// <summary>
        /// Создание чата в тг
        /// </summary>
        /// <param name="telegramMakeChat"></param>
        /// <param name="chatExist"></param>
        /// <returns>Id созданного сообщения</returns>
        Task<int> MakeChat(ApiTelegramMakeChat telegramMakeChat);
        /// <summary>
        /// Отправка сообщения к чату
        /// </summary>
        /// <param name="telegramSendMessage"></param>
        /// <returns>Id созданного сообщения</returns>
        Task<int> SendMessageInSupportChat(ApiTelegramSendMessage telegramSendMessage);

    }
}