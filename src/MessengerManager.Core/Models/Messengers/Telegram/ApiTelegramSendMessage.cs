using System;

namespace MessengerManager.Core.Models.Messengers.Telegram
{
    public class ApiTelegramSendMessage
    {
        public ApiTelegramSendMessage()
        { }

        public ApiTelegramSendMessage(string @from,
            string message,
            DateTime date,
            int messageId)
        {
            From = @from;
            Message = message;
            Date = date;
            MessageId = messageId;
        }
        public string From { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
        public int MessageId { get; set; }
        
        public override string ToString()
        {
            return $"{From}\n{Date}\n\n{Message}";
        }
    }
}