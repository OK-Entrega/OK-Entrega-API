using Domains.Entities;
using Domains.Repositories;
using Infra.Data.Contexts;
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

        public ICollection<Shipper> ReadAll(Guid companyId)
        {
            throw new NotImplementedException();
        }

        public Shipper Search(string email)
        {
            return _context.Shippers.FirstOrDefault(s => s.Email == email);
        }

        public void Create(Shipper shipper)
        {
            _context.Shippers.Add(shipper);
            _context.SaveChanges();
        }
    }
}
