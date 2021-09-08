using System;
using System.ComponentModel.DataAnnotations;

namespace MessengerManager.Domain.Entities
{
    public class Entity
    {
        [Key]
        public Guid Id { get; protected set; }
        public DateTime Created { get; protected set; } = DateTime.Now;
        public DateTime Updated { get; protected set; } = DateTime.Now;
        public bool Inactive { get; protected set; }

        public void SetUpdated()
        {
            Updated = DateTime.Now;
        }

        public void SetInactive()
        {
            Inactive = true;
        }
    }
}