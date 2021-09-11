using System.ComponentModel.DataAnnotations;

namespace MessengerManager.Domain.Entities
{
    public class ChatThreadEntity : Entity
    {
        private const int SupChatIdLength = 128;
        private const int ThreadNameLength = 128;

        private ChatThreadEntity()
        { }
        public ChatThreadEntity(string telegramSupChatId,
            string threadName, 
            int messageId)
        {
            TelegramSupChatId = telegramSupChatId;
            ThreadName = threadName;
            MessageId = messageId;
        }
        [StringLength(SupChatIdLength)]
        public string TelegramSupChatId { get; private set; }
        [StringLength(ThreadNameLength)]
        public string ThreadName { get; private set; }
        public long VkPeerId { get; private set; }
        public int MessageId { get; private set; }

        public void UpdateVkPeerId(long vkPeerId)
        {
            VkPeerId = vkPeerId;
            SetUpdated();
        }
    }
}