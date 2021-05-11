using Domains.Entities;
using System;
using System.Collections.Generic;

namespace Domains.Repositories
{
    public interface IShipperRepository
    {
        ICollection<Shipper> ReadAll(Guid companyId);
        Shipper Search(string email);
        void Create(Shipper shipper);
    }
}
