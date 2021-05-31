using Domains.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Data.Map
{
    public class OrderMap : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(o => o.Series).HasColumnType("VARCHAR(3)");
            builder.Property(o => o.Series).IsRequired();

            builder.Property(o => o.Weight).HasColumnType("DECIMAL(7, 2)");
        }
    }
}
