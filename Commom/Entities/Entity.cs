using Flunt.Notifications;
using System;

namespace Commom.Entities
{
    public abstract class Entity : Notifiable<Notification>
    {
        public Guid Id { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public Entity()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.Now;
        }
    }
}
