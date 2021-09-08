using System.ComponentModel.DataAnnotations;

namespace MessengerManager.Domain.Entities
{
    public class ChatThreadEntity : Entity
    {
        private const int SupChatIdLength = 128;
        private const int ThreadNameLength = 128;

        public ChatThreadEntity()
        { }
        public ChatThreadEntity(string supChatId,
            string threadName, 
            int messageId)
        {
            SupChatId = supChatId;
            ThreadName = threadName;
            MessageId = messageId;
        }
        [StringLength(SupChatIdLength)]
        public string SupChatId { get; set; }
        [StringLength(ThreadNameLength)]
        public string ThreadName { get; set; }
        public int MessageId { get; set; }
    }
}