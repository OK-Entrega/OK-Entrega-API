using Domains.Entities;
using System;
using System.Collections.Generic;

namespace Domains.Repositories
{
    public interface IOrder
    {
        List<Order> List(Guid idOrder, string name = null);
        Order Search(Guid id);
        Order Search(string cnpj);
        Order Add(Order order);
        Order Change(Order order);
        void Remove(Guid id);
    }
}
