using System.Collections.Generic;

namespace Domains.Entities
{
    public class Shipper : User
    {
        public string Email { get; private set; }
        public ICollection<ShipperCompany> ShipperHasCompanies { get; private set; }

        public Shipper(
            string name, 
            string password, 
            string email
        ) : base(
            name,
            password
        )
        {
            Email = email;
            ShipperHasCompanies = new List<ShipperCompany>();
        }
    }
}
