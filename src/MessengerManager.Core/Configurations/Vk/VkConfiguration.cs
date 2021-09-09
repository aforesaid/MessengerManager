namespace MessengerManager.Core.Configurations.Vk
{
    public class VkConfiguration
    {
        public VkConfiguration()
        { }
        
        public VkConfiguration(ulong applicationId, 
            string login,
            string password)
        {
            ApplicationId = applicationId;
            Login = login;
            Password = password;
        }
        public ulong ApplicationId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}