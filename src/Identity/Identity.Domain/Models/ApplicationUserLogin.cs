using Microsoft.AspNetCore.Identity;

namespace Identity.Domain.Models;

public class ApplicationUserLogin : IdentityUserLogin<int>
{
    public virtual ApplicationUser User { get; set; } = null!;
}