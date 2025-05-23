using AutoMapper;
using Common.Application.Models.Responses.PaymentSystems;
using Common.Domain.ValueObjects;
using MediatR;
using PaymentSystem.Domain.Contracts;

namespace PaymentSystem.Application.Queries.PaymentInfo.Details;

public class PaymentInfoDetailsQuery : IRequest<PaymentInfoResponse>
{
    public int OrderId { get; set; }

    public class PaymentInfoDetailsQueryHandler : IRequestHandler<PaymentInfoDetailsQuery, PaymentInfoResponse>
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMapper _mapper;

        public PaymentInfoDetailsQueryHandler(
            IPaymentRepository paymentRepository,
            IMapper mapper)
        {
            _paymentRepository = paymentRepository;
            _mapper = mapper;
        }

        public async Task<PaymentInfoResponse> Handle(
        PaymentInfoDetailsQuery request,
        CancellationToken cancellationToken)
        {
            var orderId = Common.Domain.ValueObjects.OrderId.From(request.OrderId);
            var paymentInfo = await _paymentRepository.GetPaymentInfoByOrderIdAsync(orderId, cancellationToken);

            return _mapper.Map<PaymentInfoResponse>(paymentInfo);
        }
    }
}