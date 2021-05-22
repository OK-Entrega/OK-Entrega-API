using Domains.Entities;
using System;

namespace Domains.Repositories
{
    public interface IUserRepository
    {
        User Search(Guid id);
        void Create(User user);
        //void Change(User user);
        //void Remove(Guid id);
    }
}
