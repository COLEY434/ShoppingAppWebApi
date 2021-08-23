using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoppingApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp.Infrastructure.Database.Config
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(k => k.Id);
            builder.Property(k => k.Name).HasMaxLength(100).IsRequired();
            builder.Property(k => k.Description).IsRequired();
            builder.Property(k => k.Brand).HasMaxLength(50).IsRequired();
            builder.Property(k => k.Color).HasMaxLength(20).IsRequired();
            builder.Property(k => k.Size).HasMaxLength(50).IsRequired();
            builder.Property(k => k.Price).HasPrecision(10, 2);
            builder.Property(k => k.DateAdded).HasColumnType("datetime");
            builder.Property(k => k.CategoryId).HasColumnName("category_id").IsRequired(false);
            builder.HasOne(k => k.Category).WithMany(p => p.Products).HasForeignKey(f => f.CategoryId);
        }
    }
}
