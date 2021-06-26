using Commom.Entities;
using Commom.Enum;
using System;
using System.Collections.Generic;

namespace Domains.Entities
{
    public class Company : Entity
    {
        public string Name { get; private set; }
        public string CNPJ { get; private set; }
        public string Code { get; private set; }
        public EnCompanySegment Segment { get; private set; }
        public ICollection<ShipperCompany> CompanyHasShippers { get; private set; }
        public ICollection<Order> Orders { get; private set; }
        public string UrlLogo { get; private set; }

        public Company() { }

        public Company(
            string name,
            string cnpj,
            string code,
            EnCompanySegment segment,
            string urlLogo
        )
        {
            Name = name;
            CNPJ = cnpj;
            Code = code;
            Segment = segment;
            UrlLogo = urlLogo;
            CompanyHasShippers = new List<ShipperCompany>();
        }

        public void ChangeCompany(
            string name,
            string cnpj,
            EnCompanySegment segment
        )
        {
            Name = name;
            CNPJ = cnpj;
            Segment = segment;
        }

        public void ChangeLogo(
            string urlLogo
        )
        {
            UrlLogo = urlLogo;
        }
    }
}
