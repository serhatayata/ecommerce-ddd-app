using Common.Domain.ValueObjects;
using MassTransit;
using MediatR;
using PaymentSystem.Domain.Contracts;
using PaymentSystem.Domain.Events;
using PaymentSystem.Domain.Models;

namespace PaymentSystem.Application.Commands.Payments;

public class PaymentCreateCommand : IRequest<PaymentCreateResponse>, CorrelatedBy<Guid?>
{
    public int OrderId { get; set; }
    public decimal Amount { get; set; }
    public PaymentMethod Method { get; set; }
    public Guid? CorrelationId { get; set; }

    public class PaymentCreateCommandHandler : IRequestHandler<PaymentCreateCommand, PaymentCreateResponse>
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IPublishEndpoint _publishEndpoint;

        public PaymentCreateCommandHandler(
            IPaymentRepository paymentRepository,
            IPublishEndpoint publishEndpoint)
        {
            _paymentRepository = paymentRepository;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<PaymentCreateResponse> Handle(
            PaymentCreateCommand request,
            CancellationToken cancellationToken)
        {
            // PAYMENT PROCESS ----

            var payment = new Payment(request.OrderId, request.Amount, request.Method);

            await _paymentRepository.SaveAsync(payment, cancellationToken);

            var statuses = Enum.GetValues(typeof(PaymentStatus));
            var random = new Random();
            var randomStatus = (PaymentStatus)statuses.GetValue(random.Next(statuses.Length));

            var transactionId = Guid.NewGuid().ToString();
            var transaction = new PaymentTransaction(
                request.Amount,
                DateTime.UtcNow,
                transactionId,
                randomStatus
            );

            payment.Transactions.Add(transaction);

            await _paymentRepository.UpdateAsync(payment, cancellationToken);

            // EVENTS 

            if (transaction.Status == PaymentStatus.Failed)
            {
                var paymentFailedEvent = new PaymentFailedDomainEvent(
                    request.OrderId,
                    payment.Id,
                    request.Amount,
                    request.Method,
                    transaction.TransactionId,
                    "Payment failed",
                    request.CorrelationId
                );

                await _publishEndpoint.Publish(paymentFailedEvent, cancellationToken);
            }
            else if (transaction.Status == PaymentStatus.Completed)
            {
                var paymentCompletedEvent = new PaymentCompletedDomainEvent(
                    request.OrderId,
                    payment.Id,
                    request.Amount,
                    payment.Method,
                    transaction.TransactionId,
                    request.CorrelationId
                );

                await _publishEndpoint.Publish(paymentCompletedEvent, cancellationToken);
            }

            return new PaymentCreateResponse
            {
                OrderId = payment.OrderId,
                PaymentId = payment.Id,
            };
        }
    }
}