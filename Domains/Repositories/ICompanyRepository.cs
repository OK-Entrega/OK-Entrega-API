using Domains.Entities;
using System;
using System.Collections.Generic;

namespace Domains.Repositories
{
    public interface ICompanyRepository
    {
        Company Search(Guid id);
        Company Search(string cnpj);
        Company SearchByCode(string code);
        void Create(Company company);
        void CreateShipperCompany(ShipperCompany shipperCompany);
        void Update(Company company);
        void Delete(Company company);
    }
}
