using Common.Application.Models.Responses.OrderManagements;
using Common.Domain.ValueObjects;

namespace PaymentSystem.Application.Services.OrderManagements;

public interface IOrderManagementApiService
{
    Task<OrderDetailResponse> GetOrderDetailById(OrderId orderId);
}