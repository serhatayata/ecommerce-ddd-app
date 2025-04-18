using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Common.Application.Settings;
using Common.Domain.Models;
using Identity.Application.ServiceContracts;
using Identity.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Identity.Infrastructure.Services;

internal class JwtGeneratorService : IJwtGenerator
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly ApplicationSettings applicationSettings;

    public JwtGeneratorService(
    UserManager<ApplicationUser> userManager,
    IOptions<ApplicationSettings> applicationSettings)
    {
        this.userManager = userManager;
        this.applicationSettings = applicationSettings.Value;
    }

    public async Task<string> GenerateToken(ApplicationUser user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(applicationSettings.Secret);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Email)
            }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var isAdministrator = await userManager
            .IsInRoleAsync(user, CommonModelConstants.Common.AdministratorRoleName);

        if (isAdministrator)
        {
            tokenDescriptor.Subject.AddClaim(new Claim(
                ClaimTypes.Role,
                CommonModelConstants.Common.AdministratorRoleName));
        }

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var encryptedToken = tokenHandler.WriteToken(token);

        return encryptedToken;
    }
}