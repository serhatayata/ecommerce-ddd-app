using Common.Application.Models;
using Common.Domain.ValueObjects;
using Identity.Application.Commands.ChangePassword;
using Identity.Application.Commands.Common;
using Identity.Application.Commands.RegisterUser;
using Identity.Domain.Models;

namespace Identity.Application.ServiceContracts;

public interface IIdentityService
{
    Task<Result<ApplicationUser>> Register(UserRegisterRequestModel userRequest);
    Task<Result<UserResponseModel>> Login(UserRequestModel userRequest);
    Task<Result> ChangePassword(ChangePasswordRequestModel changePasswordRequest);
    Task<Result<UserDetailResponseModel>> GetUserDetails(UserId userId);
}