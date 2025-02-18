using Microsoft.AspNetCore.Identity;

namespace Identity.Domain.Models;

public class ApplicationRoleClaim : IdentityRoleClaim<int>
{
    public virtual ApplicationRole Role { get; set; } = null!;
}