using Domains.Entities;
using Domains.Repositories;
using Infra.Data.Contexts;
using System;
using System.Collections.Generic;

namespace Infra.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DataContext _context;

        public OrderRepository(DataContext context)
        {
            _context = context;
        }

        public void Create(Order order)
        {
            throw new NotImplementedException();
        }

        public ICollection<Order> Search(ICollection<Guid> ids)
        {
            //include finish order
            throw new NotImplementedException();
        }

        public Order Search(Guid id)
        {
            //include finish order
            throw new NotImplementedException();
        }

        public Order Search(string accessKey)
        {
            throw new NotImplementedException();
        }

        public void Update(Order order)
        {
            throw new NotImplementedException();
        }

        public void Delete(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
