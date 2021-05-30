using Domains.Entities;
using System;
using System.Collections.Generic;

namespace Domains.Repositories
{
    public interface IOrderRepository
    {
        ICollection<Order> Search(ICollection<Guid> ids);
        Order Search(Guid id);
        Order Search(string accessKey);
        void Create(Order order);
        void Update(Order order);
        void Delete(Order order);
    }
}
