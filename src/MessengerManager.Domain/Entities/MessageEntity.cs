using System;

namespace MessengerManager.Domain.Entities
{
    public class MessageEntity : Entity
    {
        private const int ChatThreadNameLength = 128;
        private const int OwnerLength = 256;
        private const int TextLength = 1024;
        private MessageEntity(long vkPeerId, long messageId)
        {
            VkPeerId = vkPeerId;
            MessageId = messageId;
        }

        public MessageEntity(long userId,
            string text,
            DateTime date,
            long vkPeerId,
            long messageId)
        {
            UserId = userId;
            Text = text;
            Date = date;
            VkPeerId = vkPeerId;
            MessageId = messageId;
        }
        public long UserId { get; private set; }
        public string Text { get; private set; }
        public long VkPeerId { get; private set; }
        public long MessageId { get; private set; }
        public DateTime Date { get; private set; }
        public bool Sent { get; private set; }

        public void SetSent()
        {
            Sent = true;
            SetUpdated();
        }
    }
}