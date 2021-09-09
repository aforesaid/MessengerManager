namespace MessengerManager.Core.Models.Messengers.Vk
{
    public class ApiVkMessage
    {
        public ApiVkMessage(long messageId)
        {
            MessageId = messageId;
        }

        public ApiVkMessage(string chatThreadName,
            string title,
            ApiVkUser owner, long messageId)
        {
            ChatThreadName = chatThreadName;
            Title = title;
            Owner = owner;
            MessageId = messageId;
        }
        public string ChatThreadName { get; set; }
        public string Title { get; set; }
        public long MessageId { get; set; }
        public ApiVkUser Owner { get; set; }
    }
}