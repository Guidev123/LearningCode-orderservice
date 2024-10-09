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
    public class ProductMapping : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title).IsRequired().HasColumnType("VARCHAR(80)");
            builder.Property(x => x.Slug).IsRequired().HasColumnType("VARCHAR(80)");
            builder.Property(x => x.Description).IsRequired(false).HasColumnType("VARCHAR(255)");
            builder.Property(x => x.Price).IsRequired().HasColumnType("MONEY");
            builder.Property(x => x.IsActive).IsRequired().HasColumnType("BIT");
        }
    }
}
