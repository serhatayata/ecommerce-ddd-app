using Identity.Domain.Models;

namespace Identity.Application.ServiceContracts;

public interface IJwtGenerator
{
    Task<string> GenerateToken(ApplicationUser user);
}