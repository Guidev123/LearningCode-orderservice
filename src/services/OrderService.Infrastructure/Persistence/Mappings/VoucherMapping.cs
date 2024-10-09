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
    public class VoucherMapping : IEntityTypeConfiguration<Voucher>
    {
        public void Configure(EntityTypeBuilder<Voucher> builder)
        {
            builder.ToTable("Vouchers");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Number).IsRequired().HasColumnType("VARCHAR(36)");
            builder.Property(x => x.Title).IsRequired().HasColumnType("VARCHAR(80)");
            builder.Property(x => x.Description).IsRequired(false).HasColumnType("VARCHAR(255)");
            builder.Property(x => x.Amount).IsRequired().HasColumnType("MONEY");
            builder.Property(x => x.StartDate).IsRequired().HasColumnType("DATETIME2");
            builder.Property(x => x.EndDate).IsRequired().HasColumnType("DATETIME2");
        }
    }
}
