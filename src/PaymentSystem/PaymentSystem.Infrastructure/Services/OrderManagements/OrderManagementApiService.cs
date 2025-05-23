using System.Net.Http.Json;
using Common.Application.Models.Responses.OrderManagements;
using Common.Domain.ValueObjects;
using PaymentSystem.Application.Services.OrderManagements;

namespace PaymentSystem.Infrastructure.Services.OrderManagements;

public class OrderManagementApiService : IOrderManagementApiService
{
    private readonly HttpClient _orderManagementHttpClient;

    public OrderManagementApiService(IHttpClientFactory httpClientFactory)
    {
        _orderManagementHttpClient = httpClientFactory.CreateClient("order-management");
    }
    
    public async Task<OrderDetailResponse> GetOrderDetailById(OrderId orderId)
        => await _orderManagementHttpClient.GetFromJsonAsync<OrderDetailResponse>(
            $"orders/{orderId}");
}