using Identity.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.Persistence.EntityConfigurations;

public class RoleEntityConfiguration : IEntityTypeConfiguration<ApplicationRole>
{
    public void Configure(EntityTypeBuilder<ApplicationRole> builder)
    {
        builder.ToTable("Roles");

        // Configure primary key
        builder.HasKey(r => r.Id);

        // Configure relationships
        builder.HasMany(r => r.Claims)
            .WithOne(rc => rc.Role)
            .HasForeignKey(rc => rc.RoleId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany<ApplicationUserRole>()
            .WithOne(ur => ur.Role)
            .HasForeignKey(ur => ur.RoleId)
            .OnDelete(DeleteBehavior.Restrict);

        // Configure properties
        builder.Property(r => r.Name).HasMaxLength(256);
        builder.Property(r => r.NormalizedName).HasMaxLength(256);
    }
}