using Domains.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains.Repositories
{
    public interface IUser
    {
        User Search(Guid id);
        User Search(string email);
        User Add(User user);
        User Change(User user);
        void Remove(Guid id);
    }
}
