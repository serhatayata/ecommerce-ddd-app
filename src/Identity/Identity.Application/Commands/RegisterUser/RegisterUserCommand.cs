using System.Text.Json.Serialization;
using Common.Application.Extensions;
using Common.Application.Models;
using Identity.Application.ServiceContracts;
using Identity.Domain.Events;
using MassTransit;
using MediatR;

namespace Identity.Application.Commands.RegisterUser;

public class RegisterUserCommand : UserRegisterRequestModel, IRequest<Result>
{
    public RegisterUserCommand(
        string username,
        string email,
        string firstName,
        string lastName,
        string password,
        string confirmPassword)  // Add correlationId parameter
        : base(email, username, firstName, lastName, password)
    {
        ConfirmPassword = confirmPassword;
        Username = username;
        CorrelationId = Guid.NewGuid(); // Generate if not provided
    }

    public string Username { get; }
    public string ConfirmPassword { get; }
    [JsonIgnore]
    public Guid CorrelationId { get; }

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
            try
            {
                var result = await _identity.Register(request);

                if (!result.Succeeded)
                {
                    var userNotCreatedEvent = new UserNotCreatedDomainEvent(
                        request.Email,
                        result.Errors.FirstOrDefault(),
                        request.CorrelationId);  // Pass correlationId

                    var eventName = MessageBrokerExtensions.GetQueueName<UserNotCreatedDomainEvent>();
                    var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(
                        new Uri($"queue:{eventName}"));

                    await sendEndpoint.Send(userNotCreatedEvent, cancellationToken);
                    
                    return Result.Failure(result.Errors);
                }

                var userCreatedEvent = new UserCreatedDomainEvent(
                    result.Data.Id, 
                    result.Data.Email,
                    request.CorrelationId);  // Pass correlationId
                var userCreatedDomainEventName = MessageBrokerExtensions.GetQueueName<UserCreatedDomainEvent>();
                var successEndpoint = await _sendEndpointProvider.GetSendEndpoint(
                    new Uri($"queue:{userCreatedDomainEventName}"));

                await successEndpoint.Send(userCreatedEvent, cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                return Result.Failure(new[] { "An unexpected error occurred during registration." });
            }
        }
    }
}