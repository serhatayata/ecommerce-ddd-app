using Common.Application.Models;
using Common.Domain.ValueObjects;
using Identity.Application.Commands.ChangePassword;
using Identity.Application.Commands.Common;
using Identity.Application.Commands.RegisterUser;
using Identity.Application.ServiceContracts;
using Identity.Domain.Events;
using Identity.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace Identity.Infrastructure.Services;

internal class IdentityService : IIdentityService
{
    private const string InvalidErrorMessage = "Invalid credentials.";

    private readonly UserManager<ApplicationUser> userManager;
    private readonly IJwtGenerator jwtGenerator;

    public IdentityService(
        UserManager<ApplicationUser> userManager,
        IJwtGenerator jwtGenerator)
    {
        this.userManager = userManager;
        this.jwtGenerator = jwtGenerator;
    }

    public async Task<Result<ApplicationUser>> Register(UserRegisterRequestModel userRequest)
    {
        var user = ApplicationUser.Create(userRequest.Email, userRequest.Email, userRequest.FirstName, userRequest.LastName);

        var identityResult = await userManager.CreateAsync(
            user,
            userRequest.Password);

        if (identityResult.Succeeded)
            user.RaiseUserCreatedDomainEvent();

        return Result<ApplicationUser>.SuccessWith(user);
    }

    public async Task<Result<UserResponseModel>> Login(UserRequestModel userRequest)
    {
        var user = await userManager.FindByEmailAsync(userRequest.Email);
        if (user == null)
        {
            return InvalidErrorMessage;
        }

        var passwordValid = await userManager.CheckPasswordAsync(
            user,
            userRequest.Password);

        if (!passwordValid)
        {
            return InvalidErrorMessage;
        }

        var token = await jwtGenerator.GenerateToken(user);

        return new UserResponseModel(token);
    }

    public async Task<Result> ChangePassword(ChangePasswordRequestModel changePasswordRequest)
    {
        var user = await userManager.FindByIdAsync(changePasswordRequest.UserId.ToString());

        if (user == null)
        {
            return InvalidErrorMessage;
        }

        var identityResult = await userManager.ChangePasswordAsync(
            user,
            changePasswordRequest.CurrentPassword,
            changePasswordRequest.NewPassword);

        var errors = identityResult.Errors.Select(e => e.Description);

        return identityResult.Succeeded
            ? Result.Success
            : Result.Failure(errors);
    }

    public async Task<Result<UserDetailResponseModel>> GetUserDetails(UserId userId)
    {
        var user = await userManager.FindByIdAsync(userId.ToString());

        if (user == null)
            return InvalidErrorMessage;

        return new UserDetailResponseModel()
        {
            Id = user.Id,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
        };
    }
}