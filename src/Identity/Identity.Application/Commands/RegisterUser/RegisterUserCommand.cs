using Common.Application.Extensions;
using Common.Application.Models;
using Identity.Application.Commands.Common;
using Identity.Application.ServiceContracts;
using Identity.Domain.Events;
using MassTransit;
using MediatR;

namespace Identity.Application.Commands.RegisterUser;

public class RegisterUserCommand : UserRequestModel, IRequest<Result>
{
    public RegisterUserCommand(
    string username,
    string email,
    string password,
    string confirmPassword)
    : base(email, password)
    {
        ConfirmPassword = confirmPassword;
        Username = username;
    }

    public string Username { get; }

    public string ConfirmPassword { get; }

    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result>
    {
        private readonly IIdentityService _identity;
        private readonly ISendEndpointProvider _sendEndpointProvider;
        
        public RegisterUserCommandHandler(
        IIdentityService identity,
        ISendEndpointProvider sendEndpointProvider)
        {
            _identity = identity;
            _sendEndpointProvider = sendEndpointProvider;
        }

        public async Task<Result> Handle(
        RegisterUserCommand request,
        CancellationToken cancellationToken)
        {
            var result = await _identity.Register(request);

            if (!result.Succeeded)
                return Result.Failure(result.Errors);

            var userCreatedEvent = new UserCreatedDomainEvent(result.Data.Id, result.Data.Email);

            var userCreatedDomainEventName = MessageBrokerExtensions.GetQueueName<UserCreatedDomainEvent>();
            var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{userCreatedDomainEventName}"));

            await sendEndpoint.Send(userCreatedEvent, cancellationToken);

            return result;
        }
    }
}