using System;

namespace MessengerManager.Core.Models.Messengers.Telegram
{
    public class ApiTelegramMakeChat
    {
        public ApiTelegramMakeChat()
        {
        }

        public ApiTelegramMakeChat(string chatName)
        {
            ChatName = chatName;
        }
        public string ChatName { get; set; }
    }
}