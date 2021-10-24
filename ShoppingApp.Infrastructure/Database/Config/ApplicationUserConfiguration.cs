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
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasKey(k => k.Id);
            builder.Property(k => k.Firstname).HasMaxLength(100).IsRequired();
            builder.Property(k => k.Lastname).HasMaxLength(100).IsRequired();
            builder.Property(k => k.UserName).HasMaxLength(100).IsRequired();
            builder.Property(k => k.Email).HasMaxLength(100).IsRequired();
            builder.Property(k => k.PhoneNumber).HasMaxLength(100).IsRequired();
            builder.Property(k => k.Address).IsRequired();
        }
    }
}
