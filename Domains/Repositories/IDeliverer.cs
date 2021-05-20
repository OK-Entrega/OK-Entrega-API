using Domains.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains.Repositories
{
    public interface IShipper
    {
        Shipper Search(Guid id);
        Shipper Search(string user);
        Shipper Add(Shipper shipper);
        Shipper Change(Shipper shipper);
        void Remove(Guid id);
    }
}
