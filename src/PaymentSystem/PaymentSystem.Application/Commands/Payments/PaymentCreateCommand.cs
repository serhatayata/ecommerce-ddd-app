using MassTransit;
using MediatR;
using PaymentSystem.Application.Services.OrderManagements;
using PaymentSystem.Domain.Contracts;
using PaymentSystem.Domain.Models;

namespace PaymentSystem.Application.Commands.Payments;

public class PaymentCreateCommand : IRequest<PaymentCreateResponse>, CorrelatedBy<Guid?>
{
    public int OrderId { get; set; }
    public Guid? CorrelationId { get; set; }

    public class PaymentCreateCommandHandler : IRequestHandler<PaymentCreateCommand, PaymentCreateResponse>
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IOrderManagementApiService _orderManagementApiService;

        public PaymentCreateCommandHandler(
            IPaymentRepository paymentRepository,
            IOrderManagementApiService orderManagementApiService)
        {
            _paymentRepository = paymentRepository;
            _orderManagementApiService = orderManagementApiService;
        }

        public async Task<PaymentCreateResponse> Handle(
            PaymentCreateCommand request,
            CancellationToken cancellationToken)
        {
            var orderId = Common.Domain.ValueObjects.OrderId.From(request.OrderId);
            var order = await _orderManagementApiService.GetOrderDetailById(orderId);
            if (order == null)
                return null;

            var paymentInfo = await _paymentRepository.GetPaymentInfoByOrderIdAsync(orderId, cancellationToken);
            if (paymentInfo == null)
                return null;

            var paymentMethod = paymentInfo.Method;

            // PAYMENT PROCESS HANDLED HERE - DEMO ----
            // END

            var amount = order.OrderItems.Sum(o => o.UnitPrice * o.Quantity);
            var payment = Payment.Create(orderId, amount, paymentMethod);

            await _paymentRepository.SaveAsync(payment, cancellationToken);

            var statuses = Enum.GetValues(typeof(PaymentStatus));
            var random = new Random();
            var randomStatus = (PaymentStatus)statuses.GetValue(random.Next(statuses.Length));

            var transactionId = Guid.NewGuid().ToString();
            var transaction = new PaymentTransaction(
                amount,
                DateTime.UtcNow,
                transactionId,
                randomStatus
            );

            payment.Transactions.Add(transaction);

            await _paymentRepository.UpdateAsync(payment, cancellationToken);

            // EVENTS 

            if (transaction.Status == PaymentStatus.Failed)
                payment.MarkAsFailed(correlationId: request.CorrelationId);
            else if (transaction.Status == PaymentStatus.Completed)
                payment.MarkAsCompleted(correlationId: request.CorrelationId);

            return new PaymentCreateResponse
            {
                OrderId = payment.OrderId.Value,
                PaymentId = payment.Id,
            };
        }
    }
}