using Common.Application.Models.Responses.OrderManagements;

namespace PaymentSystem.Application.Services.OrderManagements;

public interface IOrderManagementApiService
{
    Task<OrderDetailResponse> GetOrderDetailById(int orderId);
}