using Commom.Entities;

namespace Domains.Entities
{
    public class User : Entity
    {
        public string Name { get; private set; }
        public string Password { get; private set; }
        public Shipper Shipper { get; private set; }
        public Deliverer Deliverer { get; private set; }

        public User(string name, string password)
        {
            Name = name;
            Password = password;
        }
    }
}
