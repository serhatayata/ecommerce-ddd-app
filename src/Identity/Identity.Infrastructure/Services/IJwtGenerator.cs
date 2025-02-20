using Identity.Domain.Models;

namespace Identity.Infrastructure.Services;

public interface IJwtGenerator
{
    Task<string> GenerateToken(ApplicationUser user);
}