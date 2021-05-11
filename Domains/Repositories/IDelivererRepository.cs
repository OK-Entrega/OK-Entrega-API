using Domains.Entities;
using System;

namespace Domains.Repositories
{
    public interface IDelivererRepository
    {
        Deliverer Search(string cellphoneNumber);
        void Create(Deliverer Deliverer);
        void Update(Deliverer Deliverer);
        void Delete(Guid id);
    }
}
