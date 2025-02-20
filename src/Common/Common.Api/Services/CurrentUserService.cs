using System.Security.Claims;
using Common.Application.Contracts;
using Microsoft.AspNetCore.Http;

namespace Common.Api.Services;

public class CurrentUserService : ICurrentUser
{
    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        var user = httpContextAccessor.HttpContext?.User;

        if (user == null)
            throw new InvalidOperationException("This request does not have an authenticated user.");

        var value = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        UserId = string.IsNullOrEmpty(value) ? default(int) : int.Parse(value);
    }

    public int UserId { get; }
}