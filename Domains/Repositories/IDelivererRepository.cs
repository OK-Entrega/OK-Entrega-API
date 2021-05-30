using Domains.Entities;
using System;

namespace Domains.Repositories
{
    public interface IDelivererRepository
    {
        Deliverer Search(Guid id);
        Deliverer Search(string cellphoneNumber);
        void Create(Deliverer deliverer);
        void Update(Deliverer deliverer);
        void Delete(Guid id);
    }
}
