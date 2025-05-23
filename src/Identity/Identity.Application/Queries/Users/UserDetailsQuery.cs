using Common.Domain.ValueObjects;
using Identity.Application.Commands.Common;
using Identity.Application.ServiceContracts;
using MediatR;

namespace Identity.Application.Queries.Users;

public class UserDetailsQuery : IRequest<UserDetailResponseModel>
{
    public int Id { get; set; }

    public class UserDetailsQueryHandler : IRequestHandler<UserDetailsQuery, UserDetailResponseModel>
    {
        private readonly IIdentityService _identityService;

        public UserDetailsQueryHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<UserDetailResponseModel> Handle(
            UserDetailsQuery request,
            CancellationToken cancellationToken)
        {
            var model = await _identityService.GetUserDetails(UserId.From(request.Id));

            return model.Data;
        }
    }

    
}