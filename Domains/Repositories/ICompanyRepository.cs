using Domains.Entities;
using System;
using System.Collections.Generic;

namespace Domains.Repositories
{
    public interface ICompanyRepository
    {
        List<Company> List(Guid idCompany, string name = null);
        Company Search(Guid id);
        Company Search(string cnpj);
        Company Add(Company company);
        Company Change(Company company);
        void Remove(Guid id);
        void AddShipper(ShipperCompany shipperCompany);
        void ChangeShipper(ShipperCompany shipperCompany);
        void DeleteShipper(Guid id);
    }
}
