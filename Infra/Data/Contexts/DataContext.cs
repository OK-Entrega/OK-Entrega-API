using Domains.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Contexts
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Shipper> Shippers { get; set; }
        public DbSet<ShipperCompany> ShipperCompanies { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<FinishOrder> FinishedOrders { get; set; }
        public DbSet<OccurrenceOrder> Occurrences { get; set; }
        public DbSet<Deliverer> Deliverers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Ignore<Local>();

            modelBuilder
                .Entity<Order>()
                .Property("TotalPriceNFE")
                .HasColumnType("DECIMAL");
        }
    }
}
