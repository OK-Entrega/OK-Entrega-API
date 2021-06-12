using Domains.Entities;
using Domains.Repositories;
using Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;
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

        public IQueryable<Order> Read(Guid companyId)
        {
            return _context
                .Orders
                .Include(o => o.Occurrences)
                .ThenInclude(o => o.Deliverer)
                .ThenInclude(o => o.User)
                .Include(o => o.FinishOrder)
                .ThenInclude(fo => fo.Voucher)
                .Where(o => o.CompanyId == companyId);
        }

        public Order Search(Guid id)
        {
            return _context
                .Orders
                .Include(o => o.Occurrences)
                .ThenInclude(o => o.Deliverer)
                .ThenInclude(o => o.User)
                .Include(o => o.FinishOrder)
                .ThenInclude(fo => fo.Voucher)
                .FirstOrDefault(o => o.Id == id);
        }

        public Order Search(string accessKey)
        {
            return _context
                .Orders
                .Include(o => o.Occurrences)
                .ThenInclude(o => o.Deliverer)
                .ThenInclude(o => o.User)
                .Include(o => o.FinishOrder)
                .ThenInclude(fo => fo.Voucher)
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
            _context
                .Orders
                .Remove(order);

            _context
                .SaveChanges();
        }
    }
}
