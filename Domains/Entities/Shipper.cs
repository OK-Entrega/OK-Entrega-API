using Commom.Entities;
using Flunt.Validations;
using System;
using System.Collections.Generic;

namespace Domains.Entities
{
    public class Shipper : Entity
    {
        public string Email { get; private set; }
        public string CodeEmail { get; private set; }
        public User User { get; private set; }
        public Guid UserId { get; private set; }
        public ICollection<ShipperCompany> ShipperHasCompanies { get; private set; }

        public Shipper(
            string email,
            User user
        )
        {
            Email = email;
            User = user;
            ShipperHasCompanies = new List<ShipperCompany>();
            CodeEmail = null;
        }

        public Shipper(){}

        public void ChangeEmail(string email)
        {
            Email = email;
        }

        public void RequestNewEmail(string codeEmail)
        {
            CodeEmail = codeEmail;
        }
    }
}
