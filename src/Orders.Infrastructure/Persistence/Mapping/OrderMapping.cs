using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orders.Domain.Entities;

namespace Orders.Infrastructure.Persistence.Mapping
{
    public class OrderMapping : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Number).IsRequired().HasColumnType("CHAR").HasMaxLength(8);

            builder.Property(x => x.ExternalReference).IsRequired(false).HasColumnType("VARCHAR").HasMaxLength(60);

            builder.Property(x => x.PaymentGateway).IsRequired().HasColumnType("SMALLINT");

            builder.Property(x => x.CreatedAt).IsRequired().HasColumnType("DATETIME2");

            builder.Property(x => x.UpdatedAt).IsRequired().HasColumnType("DATETIME2");

            builder.Property(x => x.Status).IsRequired().HasColumnType("SMALLINT");
            
            builder.Property(x => x.Total).IsRequired().HasColumnType("MONEY");

            builder.Property(x => x.UserId).IsRequired().HasColumnType("VARCHAR").HasMaxLength(160);

            builder.HasOne(x => x.Product).WithMany();
            builder.HasOne(x => x.Voucher).WithMany();
        }
    }
}
