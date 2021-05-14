using Domains.Entities;
using Domains.Repositories;
using Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Infra.Data.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private DataContext Contexto { get; set; }

        public CompanyRepository(DataContext context)
        {
            Contexto = context;
        }
        public Company Add(Company company)
        {
            Contexto.Companies.Add(company);
            Contexto.SaveChanges();
            return company;
        }

        public void AddShipper(ShipperCompany shipperCompany)
        {
            Contexto.ShipperCompanies.Add(shipperCompany);
            Contexto.SaveChanges();
        }

        public Company Change(Company company)
        {
            Contexto.Entry(company).State = EntityState.Modified;
            Contexto.SaveChanges();
            return company;
        }

        public void ChangeShipper(ShipperCompany shipperCompany)
        {
            Contexto.Entry(shipperCompany).State = EntityState.Modified;
            Contexto.SaveChanges();
        }

        public void DeleteShipper(Guid id)
        {
            var shipperCompany = Contexto.ShipperCompanies.Find(id);
            Contexto.ShipperCompanies.Remove(shipperCompany);
            Contexto.SaveChanges();
        }

        public List<Company> List(Guid id, string name = null)
        {
            var companies = Contexto
                .Companies
                .Include(i => i.CompanyHasShippers)
                .ToList();

            companies = companies.FindAll(i => i.CompanyHasShippers.Any(ui => ui.ShipperId == id));

            if (name != null)
                companies = companies.FindAll(i => i.Name.ToLower().Contains(name));

            return companies;
        }

        public void Remove(Guid id)
        {
            var company = Search(id);
            Contexto.Companies.Remove(company);
            Contexto.SaveChanges();
        }

        public Company Search(Guid id)
        {
            return Contexto
                .Companies
                .Include(i => i.CompanyHasShippers)
                .ThenInclude (ui => ui.Shipper)
                .FirstOrDefault(i => i.Id == id);
        }

        public Company Search(string cnpj)
        {
            return Contexto
                .Companies
                .Include(i => i.CompanyHasShippers)
                .FirstOrDefault(i => i.CNPJ == cnpj);
        }
    }
}
