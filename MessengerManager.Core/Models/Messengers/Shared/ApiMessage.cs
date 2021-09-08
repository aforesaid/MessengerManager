namespace MessengerManager.Core.Models.Messengers.Shared
{
    public class ApiMessage
    {
        public ApiMessage()
        { }

        public ApiMessage(string text, 
            string chatName,
            string chatId)
        {
            Text = text;
            ChatName = chatName;
            ChatId = chatId;
        }
        public string Text { get; set; }
        public string ChatName { get; set; }
        public string ChatId { get; set; }


        public override string ToString()
        {
            return base.ToString();
        }
    }
}