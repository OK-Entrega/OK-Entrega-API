using Domains.Entities;
using System;
using System.Linq;

namespace Domains.Repositories
{
    public interface IOrderRepository
    {
        Order Search(Guid id);
        Order Search(string accessKey);
        IQueryable<Order> Read(Guid companyId);
        void Create(Order order);
        void CreateOccurrence(OccurrenceOrder occurrence);
        void Finish(FinishOrder finishOrder);
        void Delete(Order order);
    }
}
