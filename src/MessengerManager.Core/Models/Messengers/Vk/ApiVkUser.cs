namespace MessengerManager.Core.Models.Messengers.Vk
{
    public class ApiVkUser
    {
        public ApiVkUser()
        { }

        public ApiVkUser(string name, 
            string lastName,
            string uniqueId,
            long userId)
        {
            Name = name;
            LastName = lastName;
            UniqueId = uniqueId;
            UserId = userId;
        }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string UniqueId { get; set; }
        
        public long UserId { get; set; }

        public override string ToString()
        {
            return $"{Name} {LastName} @{UniqueId}";
        }
    }
}