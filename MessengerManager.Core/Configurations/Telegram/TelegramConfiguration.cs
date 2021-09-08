namespace MessengerManager.Core.Configurations.Telegram
{
    public class TelegramConfiguration
    {
        public TelegramConfiguration()
        { }
        public TelegramConfiguration(string token, string mainChatId, string supportChatId)
        {
            Token = token;
            MainChatId = mainChatId;
            SupportChatId = supportChatId;
        }
        public string Token { get; set; }
        public string MainChatId { get; set; }
        public string SupportChatId { get; set; }
    }
}