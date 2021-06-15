using Domains.Entities;
using System;
using System.Linq;

namespace Domains.Repositories
{
    public interface IShipperRepository
    {
        IQueryable<Shipper> Read(Guid companyId);
        Shipper Search(string email);
        Shipper Search(Guid id);
        void Create(Shipper shipper);
        void Update(Shipper shipper);
    }
}
