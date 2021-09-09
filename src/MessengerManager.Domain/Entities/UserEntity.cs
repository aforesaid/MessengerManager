namespace MessengerManager.Domain.Entities
{
    public class UserEntity : Entity
    {
        private UserEntity()
        { }

        public UserEntity(string name,
            string lastName,
            string uniqueId,
            long userId)
        {
            Name = name;
            LastName = lastName;
            UniqueId = uniqueId;
            UserId = userId;
        }
        public string Name { get; private set; }
        public string LastName { get; private set; }
        public string UniqueId { get; private set; }
        public long UserId { get; private set; }
    }
}