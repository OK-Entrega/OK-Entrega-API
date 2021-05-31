using Domains.Entities;
using System;

namespace Domains.Repositories
{
    public interface IOrderRepository
    {
        Order Search(Guid id);
        Order Search(string accessKey);
        void Create(Order order);
        void CreateOccurrence(OccurrenceOrder occurrence);
        void Finish(FinishOrder finishOrder);
        void Delete(Order order);
    }
}
