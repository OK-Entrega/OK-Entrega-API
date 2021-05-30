using Domains.Entities;
using System;
using System.Collections.Generic;

namespace Domains.Repositories
{
    public interface IShipperRepository
    {
        Shipper Search(string email);
        Shipper Search(Guid id);
        void Create(Shipper shipper);
        void Update(Shipper shipper);
    }
}
