using Domains.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Contexts
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Shipper> Shippers { get; set; }
        public DbSet<Deliverer> Deliverers { get; set; }
    }
}
