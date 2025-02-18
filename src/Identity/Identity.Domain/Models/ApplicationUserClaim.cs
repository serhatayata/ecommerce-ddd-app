using Microsoft.AspNetCore.Identity;

namespace Identity.Domain.Models;

public class ApplicationUserClaim : IdentityUserClaim<int>
{
    public virtual ApplicationUser User { get; set; } = null!;
}