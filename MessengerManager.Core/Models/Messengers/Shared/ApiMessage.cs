using System;

namespace MessengerManager.Core.Models.Messengers.Shared
{
    public class ApiMessage
    {
        public ApiMessage()
        {
        }

        public ApiMessage(string from, 
            string text, 
            string chatName,
            DateTime date)
        {
            Text = text;
            ChatName = chatName;
            Date = date;
        }
        public string From { get; set; }
        public DateTime Date { get; set; }
        public string Text { get; set; }
        public string ChatName { get; set; }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}