using Domains.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains.Repositories
{
    public interface IDeliverer
    {
        Deliverer Search(Guid id);
        Deliverer Search(string user);
        Deliverer Add(Deliverer deliverer);
        Deliverer Change(Deliverer deliverer);
        void Remove(Guid id);
    }
}
