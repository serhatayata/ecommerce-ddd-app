using AutoMapper;
using MediatR;
using PaymentSystem.Application.Queries.Payments.Common;
using PaymentSystem.Application.Queries.PaymentTransactions.Common;
using PaymentSystem.Domain.Contracts;

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
            var paymentTransactions = await _paymentRepository.GetTransactionsByPaymentIdAsync(request.PaymentId, cancellationToken);
            
            return _mapper.Map<List<PaymentTransactionResponse>>(paymentTransactions);
        }
    }
}