using AutoMapper;
using MediatR;
using PaymentSystem.Application.Queries.PaymentTransactions.Common;
using PaymentSystem.Domain.Contracts;
using ValueObjects = Common.Domain.ValueObjects;

namespace PaymentSystem.Application.Queries.PaymentTransactions.Details;

public class PaymentTransactionDetailsQuery : IRequest<List<PaymentTransactionResponse>>
{
    public int PaymentId { get; set; }

    public class PaymentTransactionDetailsQueryHandler : IRequestHandler<PaymentTransactionDetailsQuery, List<PaymentTransactionResponse>>
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMapper _mapper;

        public PaymentTransactionDetailsQueryHandler(
            IPaymentRepository paymentRepository,
            IMapper mapper)
        {
            _paymentRepository = paymentRepository;
            _mapper = mapper;
        }

        public async Task<List<PaymentTransactionResponse>> Handle(
        PaymentTransactionDetailsQuery request,
        CancellationToken cancellationToken)
        {
            var paymentId = ValueObjects.PaymentId.From(request.PaymentId);
            var paymentTransactions = await _paymentRepository.GetTransactionsByPaymentIdAsync(paymentId, cancellationToken);
            
            return _mapper.Map<List<PaymentTransactionResponse>>(paymentTransactions);
        }
    }
}