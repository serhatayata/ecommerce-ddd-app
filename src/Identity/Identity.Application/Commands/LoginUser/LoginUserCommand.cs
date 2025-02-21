using Common.Application.Models;
using Identity.Application.Commands.Common;
using Identity.Application.ServiceContracts;
using MediatR;

namespace Identity.Application.Commands.LoginUser;

public class LoginUserCommand : UserRequestModel, IRequest<Result<UserResponseModel>>
{
    public LoginUserCommand(string email, string password)
        : base(email, password)
    {
    }

    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Result<UserResponseModel>>
    {
        private readonly IIdentityService identity;

        public LoginUserCommandHandler(IIdentityService identity)
            => this.identity = identity;

        public async Task<Result<UserResponseModel>> Handle(
            LoginUserCommand request,
            CancellationToken cancellationToken)
            => await this.identity.Login(request);
    }
}