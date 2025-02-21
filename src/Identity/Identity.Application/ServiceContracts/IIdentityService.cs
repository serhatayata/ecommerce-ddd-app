using Common.Application.Models;
using Identity.Application.Commands;
using Identity.Application.Commands.ChangePassword;
using Identity.Application.Commands.Common;
using Identity.Domain.Models;

namespace Identity.Application.ServiceContracts;

public interface IIdentityService
{
    Task<Result<ApplicationUser>> Register(UserRequestModel userRequest);
    Task<Result<UserResponseModel>> Login(UserRequestModel userRequest);
    Task<Result> ChangePassword(ChangePasswordRequestModel changePasswordRequest);
    Task<Result<UserDetailResponseModel>> GetUserDetails(int userId);
}