using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Infrastructure.Persistence.Mappings
{
    public class OrderMapping : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Number).IsRequired().HasColumnType("CHAR(8)");
            builder.Property(x => x.ExternalReference).IsRequired(false).HasColumnType("VARCHAR(60)");
            builder.Property(x => x.PaymentGateway).IsRequired().HasColumnType("SMALLINT");
            builder.Property(x => x.CreatedAt).IsRequired().HasColumnType("DATETIME2");
            builder.Property(x => x.UpdatedAt).IsRequired().HasColumnType("DATETIME2");
            builder.Property(x => x.Status).IsRequired().HasColumnType("SMALLINT");
            builder.Property(x => x.ExternalReference).IsRequired().HasColumnType("VARCHAR(60)");
            builder.Property(x => x.UserId).IsRequired().HasColumnType("UNIQUEIDENTIFIER");
            builder.HasOne(x => x.Product).WithMany();
            builder.HasOne(x => x.Voucher).WithMany();


        }
    }
}
