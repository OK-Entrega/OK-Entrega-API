using Commom.Entities;
using Commom.Enum;
using Flunt.Validations;
using System.Collections.Generic;

namespace Domains.Entities
{
    public class Company : Entity
    {
        public string Name { get; private set; }
        public string CNPJ { get; private set; }
        public EnCompanySegment Segment { get; private set; }
        public ICollection<ShipperCompany> CompanyHasShippers { get; private set; }

        public Company() { }

        public Company(
            string name,
            string cnpj,
            EnCompanySegment segment
        )
        {
            name = name.Trim();
            cnpj = cnpj.Trim().Replace("-", "").Replace(".", "").Replace("/", "");

            AddNotifications(new Contract<Company>()
                .Requires()
                .IsTrue((name.Length > 2 && name.Length < 41), "Nome", "O nome da empresa deve ter entre 3 à 40 caracteres!")
                .IsTrue(cnpj.Length == 14, "CNPJ", "O CNPJ da empresa deve conter 14 caracteres!")
            );

            if (IsValid)
            {
                Name = name;
                CNPJ = cnpj;
                Segment = segment;
                CompanyHasShippers = new List<ShipperCompany>();
            }
        }
    }
}
