using Microsoft.AspNetCore.Identity;

namespace Identity.Domain.Models;

public class ApplicationUserToken : IdentityUserToken<int>
{
    public virtual ApplicationUser User { get; set; } = null!;
}