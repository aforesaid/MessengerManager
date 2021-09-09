using System.Threading.Tasks;

namespace MessengerManager.Core.Services.VkManager
{
    public interface IVkBotManager
    {
        Task Test();

        //TODO: читать все чаты, фильтровать только нужные
        //TODO: все сообщения по фильтрованным чатам переносить в Telegram
    }
}