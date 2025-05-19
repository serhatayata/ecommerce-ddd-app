using MassTransit;
using MediatR;
using PaymentSystem.Domain.Contracts;

namespace PaymentSystem.Application.Commands.PaymentInformation;

public class PaymentInfoCreateCommand : IRequest<PaymentInfoCreateResponse>, CorrelatedBy<Guid?>
{
    public int OrderId { get; set; }
    public string CardNumber { get; set; }
    public string IBAN { get; set; }
    public string CVV { get; set; }
    public string HolderName { get; set; }
    public Guid? CorrelationId { get; set; }

    public class PaymentCreateCommandHandler : IRequestHandler<PaymentInfoCreateCommand, PaymentInfoCreateResponse>
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMediator _mediator;

        public PaymentCreateCommandHandler(
        IPaymentRepository paymentRepository,
        IMediator mediator)
        {
            _paymentRepository = paymentRepository;
            _mediator = mediator;
        }
        
        public async Task<PaymentInfoCreateResponse> Handle(
        PaymentInfoCreateCommand request,
        CancellationToken cancellationToken)
        {
            var paymentInfo = new Domain.Models.PaymentInfo(
                request.OrderId,
                request.CardNumber,
                request.IBAN,
                request.CVV,
                request.HolderName
            );

            var id = await _paymentRepository.CreatePaymentInfoAsync(paymentInfo, cancellationToken);

            return new PaymentInfoCreateResponse
            {
                Id = id,
                OrderId = paymentInfo.OrderId,
                CardNumber = paymentInfo.CardNumber,
                IBAN = paymentInfo.IBAN,
                CVV = paymentInfo.CVV,
                HolderName = paymentInfo.HolderName,
                CreatedAt = paymentInfo.CreatedAt
            };
        }
    }
}