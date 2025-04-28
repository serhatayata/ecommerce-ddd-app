using AutoMapper;
using MediatR;
using PaymentSystem.Application.Queries.Payments.Common;
using PaymentSystem.Domain.Contracts;

namespace PaymentSystem.Application.Queries.Payments.Details;

public class PaymentDetailsQuery : IRequest<PaymentResponse>
{
    public int Id { get; set; }

    public class PaymentDetailsQueryHandler : IRequestHandler<PaymentDetailsQuery, PaymentResponse>
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMapper _mapper;

        public PaymentDetailsQueryHandler(
            IPaymentRepository paymentRepository,
            IMapper mapper)
        {
            _paymentRepository = paymentRepository;
            _mapper = mapper;
        }

        public async Task<PaymentResponse> Handle(
            PaymentDetailsQuery request,
            CancellationToken cancellationToken)
        {
            var payment = await _paymentRepository.GetByIdAsync(request.Id, cancellationToken);
            
            return _mapper.Map<PaymentResponse>(payment);
        }
    }
}