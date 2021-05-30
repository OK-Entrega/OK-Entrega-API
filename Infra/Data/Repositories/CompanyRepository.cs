using Domains.Entities;
using Domains.Repositories;
using Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Infra.Data.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly DataContext _context;

        public CompanyRepository(DataContext context)
        {
            _context = context;
        }

        public Company Search(Guid companyId)
        {
            return
                _context
                .Companies
                .Include(c => c.CompanyHasShippers)
                .ThenInclude(cs => cs.Shipper)
                .ThenInclude(s => s.User)
                .FirstOrDefault(c => c.Id == companyId);
        }

        public Company Search(string cnpj)
        {
            return
                _context
                .Companies
                .Include(c => c.CompanyHasShippers)
                .ThenInclude(cs => cs.Shipper)
                .ThenInclude(s => s.User)
                .FirstOrDefault(c => c.CNPJ == cnpj);
        }

        public Company SearchByCode(string code)
        {
            return
                _context
                .Companies
                .Include(c => c.CompanyHasShippers)
                .ThenInclude(cs => cs.Shipper)
                .ThenInclude(s => s.User)
                .FirstOrDefault(c => c.Code == code);
        }

        public void Create(Company company)
        {
            _context
                .Companies
                .Add(company);

            _context
                .SaveChanges();
        }

        public void Update(Company company)
        {
            _context
                .Update(company); 
            _context
                .SaveChanges();
        }

        public void CreateShipperCompany(ShipperCompany shipperCompany)
        {
            _context
                .ShipperCompanies
                .Add(shipperCompany);
            _context
                .SaveChanges();
        }

        public void Delete(Company company)
        {
            _context
                .Companies
                .Remove(company);

            _context
                .SaveChanges();
        }
    }
}
