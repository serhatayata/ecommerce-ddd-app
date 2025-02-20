using Common.Application.Contracts;
using Common.Application.Models;
using Identity.Application.ServiceContracts;
using MediatR;

namespace Identity.Application.Commands.ChangePassword;

public class ChangePasswordCommand : IRequest<Result>
{
    public string CurrentPassword { get; set; } = default!;

    public string NewPassword { get; set; } = default!;

    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Result>
    {
        private readonly IIdentityService identity;
        private readonly ICurrentUser currentUser;

        public ChangePasswordCommandHandler(
            IIdentityService identity,
            ICurrentUser currentUser)
        {
            this.identity = identity;
            this.currentUser = currentUser;
        }

        public async Task<Result> Handle(
            ChangePasswordCommand request,
            CancellationToken cancellationToken)
            => await identity.ChangePassword(new ChangePasswordRequestModel(
                currentUser.UserId,
                request.CurrentPassword,
                request.NewPassword));
    }
}