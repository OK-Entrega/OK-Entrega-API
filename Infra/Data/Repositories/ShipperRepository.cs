using Domains.Entities;
using Domains.Repositories;
using Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infra.Data.Repositories
{
    public class ShipperRepository : IShipperRepository
    {
        private readonly DataContext _context;

        public ShipperRepository(DataContext context)
        {
            _context = context;
        }

        public IQueryable<Shipper> Read(Guid companyId)
        {
            return _context
                .Shippers
                .Include(s => s.User)
                .Include(s => s.ShipperHasCompanies)
                .Where(s => s.ShipperHasCompanies.Any(shc => shc.CompanyId == companyId))
                .OrderBy(s => s.User.Name);
        }

        public Shipper Search(string email)
        {
            return
                _context
                .Shippers
                .Include(s => s.User)
                .FirstOrDefault(s => s.Email == email);
        }

        public Shipper Search(Guid id)
        {
            return
                _context
                .Shippers
                .Include(s => s.User)
                .FirstOrDefault(s => s.Id == id);
        }

        public void Create(Shipper shipper)
        {
            _context
                .Shippers
                .Add(shipper);
            _context
                .SaveChanges();
        }

        public void Update(Shipper shipper)
        {
            _context.Entry(shipper).State = EntityState.Modified;
            _context
                .SaveChanges();
        }
    }
}
