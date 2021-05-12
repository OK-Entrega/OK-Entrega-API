using Domains.Entities;
using System;

namespace Domains.Repositories
{
    public interface IDelivererRepository
    {
        Deliverer Search(Guid id);
        Deliverer Search(string user);
        Deliverer Add(Deliverer deliverer);
        Deliverer Change(Deliverer deliverer);
        void Remove(Guid id);
    }
}
