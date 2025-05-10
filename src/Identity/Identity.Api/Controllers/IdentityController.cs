using Common.Api.Controllers;
using Identity.Application.Commands.ChangePassword;
using Identity.Application.Commands.Common;
using Identity.Application.Commands.LoginUser;
using Identity.Application.Commands.RegisterUser;
using Identity.Application.Queries.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IdentityController : BaseApiController
{
    [HttpPost]
    [Route(nameof(Register))]
    public async Task<ActionResult> Register(
    RegisterUserCommand command)
    {
        return await Send(command);
    }

    [HttpPost]
    [Route(nameof(Login))]
    public async Task<ActionResult<UserResponseModel>> Login(
    LoginUserCommand command)
    {
        return await Send(command);
    }

    [HttpPut]
    [Authorize]
    [Route(nameof(ChangePassword))]
    public async Task<ActionResult> ChangePassword(
    ChangePasswordCommand command)
    {
        return await Send(command);
    }

    [HttpGet]
    [Route(nameof(GetUserDetails))]
    public async Task<ActionResult<UserDetailResponseModel>> GetUserDetails(
    UserDetailsQuery query)
    {
        return await Send(query);
    }
}