using ContactSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactSystem.Infrastructure.Persistance.Configurations;

public class UserConfigurations : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(u => u.UserId);
        builder.Property(u => u.FirstName).IsRequired().HasMaxLength(50);
        builder.Property(u => u.LastName).IsRequired(false).HasMaxLength(50);

        builder.HasIndex(u => u.UserName).IsUnique();
        builder.Property(u => u.UserName).IsRequired().HasMaxLength(50);

        builder.HasIndex(u => u.Email).IsUnique();
        builder.Property(u => u.Email).IsRequired().HasMaxLength(100);
       
        builder.Property(u => u.Password).IsRequired().HasMaxLength(100);
        builder.Property(u => u.PhoneNumber).IsRequired().HasMaxLength(15);
        builder.HasIndex(u => u.PhoneNumber).IsUnique();
        builder.Property(u => u.Salt).IsRequired().HasMaxLength(50);

        builder.HasMany(u => u.Contacts)
               .WithOne(c => c.User)
               .HasForeignKey(c => c.UserId);

        builder.HasMany(u => u.RefreshTokens)
            .WithOne(rt => rt.User)
            .HasForeignKey(rt => rt.UserId);
           
    }
}
