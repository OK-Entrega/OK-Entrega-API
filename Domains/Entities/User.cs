using Commom.Entities;

namespace Domains.Entities
{
    public class User : Entity
    {
        public string Name { get; private set; }
        public string Password { get; private set; }
        public bool Active { get; private set; } = false;
        public Shipper Shipper { get; private set; }
        public Deliverer Deliverer { get; private set; }

        public User(
            string name,
            string password
        )
        {
            Name = name;
            Password = password;
            Active = false;
        }

        public void ChangePassword(string password)
        {
            Password = password;
        }

        public void ChangeName(string name)
        {
            Name = name;
        }

        public void TurnActive()
        {
            Active = true;
        }
    }
}
