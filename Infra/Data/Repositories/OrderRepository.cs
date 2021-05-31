using Domains.Entities;
using Domains.Repositories;
using Infra.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infra.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DataContext _context;

        public OrderRepository(DataContext context)
        {
            _context = context;
        }

        public Order Search(Guid id)
        {
            return _context
                .Orders
                .Find(id);
        }

        public Order Search(string accessKey)
        {
            return _context
                .Orders
                .FirstOrDefault(o => o.AccessKey == accessKey);
        }

        public void Create(Order order)
        {
            _context
                .Orders
                .Add(order);

            _context
                .SaveChanges();
        }

        public void CreateOccurrence(OccurrenceOrder occurrenceOrder)
        {
            _context
                .Occurrences
                .Add(occurrenceOrder);

            _context
                .SaveChanges();
        }

        public void Finish(FinishOrder finishOrder)
        {
            _context
                .FinishedOrders
                .Add(finishOrder);

            _context
                .SaveChanges();
        }

        public void Delete(Order order)
        {
            _context.Orders.Remove(order);
            _context.SaveChanges();
        }
    }
}
