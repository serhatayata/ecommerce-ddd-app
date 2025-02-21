using Identity.Domain;
using Identity.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Identity.Infrastructure.Persistence;

public class IdentityDbContext : IdentityDbContext<
    ApplicationUser, 
    ApplicationRole, 
    int,
    ApplicationUserClaim,
    ApplicationUserRole,
    ApplicationUserLogin,
    ApplicationRoleClaim,
    ApplicationUserToken>, IIdentityDbContext
{
    public IdentityDbContext(DbContextOptions<IdentityDbContext> options)
        : base(options)
    {
    }

    public DbSet<ApplicationUser> Users { get; set; } = default!;
    public DbSet<ApplicationRole> Roles { get; set; } = default!;
    public DbSet<ApplicationUserRole> UserRoles { get; set; } = default!;
    public DbSet<ApplicationUserClaim> UserClaims { get; set; } = default!;
    public DbSet<ApplicationRoleClaim> RoleClaims { get; set; } = default!;
    public DbSet<ApplicationUserLogin> UserLogins { get; set; } = default!;
    public DbSet<ApplicationUserToken> UserTokens { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        builder.HasDefaultSchema("identity");

        base.OnModelCreating(builder);
    }
}