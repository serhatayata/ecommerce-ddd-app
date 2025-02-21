using Common.Application.Models;
using Identity.Application.Commands.Common;
using Identity.Application.ServiceContracts;
using MediatR;

namespace Identity.Application.Commands.RegisterUser;

public class RegisterUserCommand : UserRequestModel, IRequest<Result>
{
    public RegisterUserCommand(
        string email,
        string password,
        string confirmPassword)
        : base(email, password)
        => ConfirmPassword = confirmPassword;

    public string ConfirmPassword { get; }

    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result>
    {
        private readonly IIdentityService identity;

        public RegisterUserCommandHandler(IIdentityService identity)
            => this.identity = identity;

        public async Task<Result> Handle(
            RegisterUserCommand request,
            CancellationToken cancellationToken)
            => await this.identity.Register(request);
    }
}