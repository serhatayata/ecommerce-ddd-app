using Common.Domain.Models;
using Common.Infrastructure.Persistence;
using Identity.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace Identity.Infrastructure.Persistence;

internal class IdentityDbInitializer : DbInitializer
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly RoleManager<ApplicationRole> roleManager;

    public IdentityDbInitializer(
        IdentityDbContext db,
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager)
        : base(db)
    {
        this.userManager = userManager;
        this.roleManager = roleManager;
    }

    public override void Initialize()
    {
        base.Initialize();

        SeedAdministrator();
    }

    private void SeedAdministrator()
        => Task
            .Run(async () =>
            {
                var existingRole = await roleManager.FindByNameAsync(CommonModelConstants.Common.AdministratorRoleName);

                if (existingRole != null)
                    return;

                var adminRole = new ApplicationRole(CommonModelConstants.Common.AdministratorRoleName);

                await roleManager.CreateAsync(adminRole);

                var adminUser = ApplicationUser.Create("admin@store.com", string.Empty, string.Empty, string.Empty);

                await userManager.CreateAsync(adminUser, "Secret.1");
                await userManager.AddToRoleAsync(adminUser, CommonModelConstants.Common.AdministratorRoleName);
            })
            .GetAwaiter()
            .GetResult();
}