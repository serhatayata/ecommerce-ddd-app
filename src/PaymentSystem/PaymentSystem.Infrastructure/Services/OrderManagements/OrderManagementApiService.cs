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
    {
        var url = $"orders/detail?Id={orderId.Value}";
        try
        {
            var result = await _orderManagementHttpClient.GetFromJsonAsync<OrderDetailResponse>(url);
            return result;
        }
        catch (System.Exception ex)
        {
            throw;
        }
    }
}