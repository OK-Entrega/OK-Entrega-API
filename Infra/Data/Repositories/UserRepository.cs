using Domains.Entities;
using Domains.Repositories;
using Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System;

namespace Infra.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public User Search(Guid id)
        {
            return _context.Users.Find(id);
        }

        public void Create(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void Update(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var user = Search(id);
            if (user != null)
                _context.Users.Remove(user);
            _context.SaveChanges();
        }
    }
}
