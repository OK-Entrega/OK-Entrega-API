using Commom.Entities;
using Flunt.Validations;
using System;
using System.Collections.Generic;

namespace Domains.Entities
{
    public class Shipper : Entity
    {
        public string Email { get; private set; }
        public User User { get; private set; }
        public Guid UserId { get; private set; }
        public ICollection<ShipperCompany> ShipperHasCompanies { get; private set; }

        public Shipper(
            string email,
            Guid userId
        )
        {
            email = email.Trim().ToLower();

            AddNotifications(new Contract<Shipper>()
                .Requires()
                .IsEmail(email, "Email", "Email inválido!")
            );

            if (IsValid)
            {
                Email = email;
                UserId = userId;
                ShipperHasCompanies = new List<ShipperCompany>();
            }
        }
    }
}
