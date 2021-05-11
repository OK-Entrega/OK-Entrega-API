using Domains.Entities;
using System;

namespace Domains.Repositories
{
    public interface IUserRepository
    {
        User Search(Guid id);
        void Create(User user);
        void Update(User user);
        void Delete(Guid id);
    }
}
