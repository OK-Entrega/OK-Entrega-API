using Domains.Entities;
using Domains.Repositories;
using Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Infra.Data.Repositories
{
    public class DelivererRepository : IDelivererRepository
    {
        private readonly DataContext _context;

        public DelivererRepository(DataContext context)
        {
            _context = context;
        }

        public Deliverer Search(Guid id)
        {
            throw new NotImplementedException();
        }

        public Deliverer Search(string cellphoneNumber)
        {
            return _context.Deliverers.Include(d => d.User).FirstOrDefault(d => d.CellphoneNumber == cellphoneNumber);
        }

        public Deliverer Add(Deliverer deliverer)
        {
            _context.Deliverers.Add(deliverer);
            _context.SaveChanges();
            return deliverer;
        }

        public Deliverer Change(Deliverer deliverer)
        {
            throw new NotImplementedException();
        }

        public void Remove(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
