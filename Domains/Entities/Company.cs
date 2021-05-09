using Commom.Entities;
using Commom.Enum;
using System.Collections.Generic;

namespace Domains.Entities
{
    public class Company : Entity
    {
        public string Name { get; private set; }
        public string CNPJ { get; private set; }
        public EnCompanySegment Segment { get; private set; }
        public ICollection<ShipperCompany> CompanyHasShippers { get; private set; }

        public Company(){}

        public Company(
            string name,
            string cnpj,
            EnCompanySegment segment
        )
        {
            Name = name;
            CNPJ = cnpj;
            Segment = segment;
            CompanyHasShippers = new List<ShipperCompany>();
        }
    }
}
