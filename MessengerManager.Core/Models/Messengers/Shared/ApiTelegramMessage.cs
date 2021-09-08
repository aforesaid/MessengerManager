namespace MessengerManager.Core.Models.Messengers.Shared
{
    public class ApiTelegramMessage
    {
        public ApiTelegramMessage()
        {
        }

        public ApiTelegramMessage(string text, 
            string chatId, int? replyMessageId = null)
        {
            Text = text;
            ChatId = chatId;
            ReplyMessageId = replyMessageId;
        }
        public string Text { get; set; }
        public string ChatId { get; set; }
        public int? ReplyMessageId { get; set; }


        public override string ToString()
        {
            return base.ToString();
        }
    }
}