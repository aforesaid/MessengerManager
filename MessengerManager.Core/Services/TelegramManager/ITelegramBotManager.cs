using System.Threading.Tasks;
using MessengerManager.Core.Models.Messengers.Shared;

namespace MessengerManager.Core.Services.TelegramManager
{
    public interface ITelegramBotManager
    {
        Task<int> SendMessage(ApiTelegramMessage telegramMessage);

        //TODO: создавать в канале тред
        //TODO: писать в этот тред комментарий - сообщение
    }
}