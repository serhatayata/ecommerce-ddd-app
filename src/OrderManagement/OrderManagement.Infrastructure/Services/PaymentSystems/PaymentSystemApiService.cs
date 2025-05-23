using Common.Application.Models.Responses.PaymentSystems;
using Common.Infrastructure.Extensions;
using OrderManagement.Application.Services.PaymentSystems;

namespace OrderManagement.Infrastructure.Services.PaymentSystems;

public class PaymentSystemApiService : IPaymentSystemApiService
{
    private readonly HttpClient _paymentSystemHttpClient;

    public PaymentSystemApiService(
    IHttpClientFactory httpClientFactory)
    {
        _paymentSystemHttpClient = httpClientFactory.CreateClient("payment-system");
    }

    #region PaymentInfo
    public async Task<PaymentInfoCreateResponse> CreatePaymentInfoAsync(
    int orderId,
    string cardNumber,
    string iban,
    string cvv,
    string holderName,
    string expirationDate,
    CancellationToken cancellationToken)
        => await _paymentSystemHttpClient.PostAsync<PaymentInfoCreateResponse, object>(
            "payments/info/create", 
            new
            {
                OrderId = orderId,
                CardNumber = cardNumber,
                IBAN = iban,
                CVV = cvv,
                HolderName = holderName,
                ExpirationDate = expirationDate
            },
            cancellationToken);
    #endregion
}