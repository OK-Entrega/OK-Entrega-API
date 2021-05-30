using Domains.Entities;
using Domains.Repositories;
using Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

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
            return
                _context
                    .Users
                    .Include(u => u.Deliverer)
                    .Include(u => u.Shipper)
                    .FirstOrDefault(u => u.Id == id);
        }

        public void Create(User user)
        {
            _context
                .Users
                .Add(user);
            _context
                .SaveChanges();
        }

        public void Update(User user)
        {
            _context
                .Entry(user).State = EntityState.Modified;
            _context
                .SaveChanges();
        }

        public void Delete(User user)
        {
            _context
                .Users
                .Remove(user);
            _context
                .SaveChanges();
        }
    }
}
