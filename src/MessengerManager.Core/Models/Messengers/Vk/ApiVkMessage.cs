using System;
using VkNet.Model;

namespace MessengerManager.Core.Models.Messengers.Vk
{
    public class ApiVkMessage
    {
        public ApiVkMessage()
        { }

        public ApiVkMessage(long vkPeerId,
            string title,
            long? userId, 
            long? messageId,
            DateTime? date)
        {
            VkPeerId = vkPeerId;
            Title = title;
            UserId = userId;
            MessageId = messageId;
            Date = date;
        }
        public long VkPeerId { get; set; }
        public string Title { get; set; }
        public long? MessageId { get; set; }
        public DateTime? Date { get; set; }
        public long? UserId { get; set; }
    }
}