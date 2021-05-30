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
            return 
                _context
                .Deliverers
                .Include(d => d.User)
                .FirstOrDefault(d => d.CellphoneNumber == cellphoneNumber);
        }

        public void Create(Deliverer deliverer)
        {
            _context
                .Deliverers
                .Add(deliverer);
            _context
                .SaveChanges();
        }

        public void Update(Deliverer deliverer)
        {
            _context.Entry(deliverer).State = EntityState.Modified;
            _context
                .SaveChanges();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
