using Domains.Entities;
using System;
using System.Linq;

namespace Domains.Repositories
{
    public interface IDelivererRepository
    {
        IQueryable<Deliverer> Read();
        Deliverer Search(Guid id);
        Deliverer Search(string cellphoneNumber);
        void Create(Deliverer deliverer);
        void Update(Deliverer deliverer);
        void Delete(Guid id);
    }
}
