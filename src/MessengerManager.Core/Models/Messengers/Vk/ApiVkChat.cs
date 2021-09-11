namespace MessengerManager.Core.Models.Messengers.Vk
{
    public class ApiVkChat
    {
        public ApiVkChat()
        { }

        public ApiVkChat(long vkPeerId,
            string chatThreadName)
        {
            VkPeerId = vkPeerId;
            ChatThreadName = chatThreadName;
        }
        public long VkPeerId { get; set; }
        public string ChatThreadName { get; set; }
    }
}