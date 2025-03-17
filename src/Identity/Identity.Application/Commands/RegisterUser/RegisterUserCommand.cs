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
        private readonly IMediator _mediator;
        
        public RegisterUserCommandHandler(
        IIdentityService identity,
        IMediator mediator)
        {
            _identity = identity;
            _mediator = mediator;
        }

        public async Task<Result> Handle(
        RegisterUserCommand request,
        CancellationToken cancellationToken)
        {
            try
            {
                var result = await _identity.Register(request);

                if (result.Data.DomainEvents.Any())
                    foreach (var domainEvent in result.Data.DomainEvents)
                        await _mediator.Publish(domainEvent, cancellationToken);                          

                return result;
            }
            catch (Exception ex)
            {
                return Result.Failure(new[] { "An unexpected error occurred during registration." });
            }
        }
    }
}