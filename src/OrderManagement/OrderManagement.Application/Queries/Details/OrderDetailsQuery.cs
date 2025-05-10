using AutoMapper;
using MediatR;
using OrderManagement.Application.Queries.Common;
using OrderManagement.Domain.Contracts;

namespace OrderManagement.Application.Queries.Details;

public class OrderDetailsQuery : IRequest<OrderResponse>
{
    public int Id { get; set; }

    public class OrderDetailsQueryHandler : IRequestHandler<OrderDetailsQuery, OrderResponse>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrderDetailsQueryHandler(
            IOrderRepository orderRepository,
            IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<OrderResponse> Handle(
            OrderDetailsQuery request,
            CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdWithItemsAsync(request.Id, cancellationToken);
            
            return _mapper.Map<OrderResponse>(order);
        }
    }
}