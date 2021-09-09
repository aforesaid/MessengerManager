namespace MessengerManager.Core.Configurations.Vk
{
    public class VkConfiguration
    {
        public VkConfiguration()
        { }
        
        public VkConfiguration(string login,
            string password, string myVkUsername)
        {
            Login = login;
            Password = password;
            MyVkUsername = myVkUsername;
        }
        public string Login { get; set; }
        public string Password { get; set; }
        public string MyVkUsername { get; set; }
    }
}