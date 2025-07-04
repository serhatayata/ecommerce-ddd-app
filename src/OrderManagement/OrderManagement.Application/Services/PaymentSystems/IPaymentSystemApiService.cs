using Common.Application.Models.Responses.PaymentSystems;

namespace OrderManagement.Application.Services.PaymentSystems;

public interface IPaymentSystemApiService
{
    public Task<PaymentInfoCreateResponse> CreatePaymentInfoAsync(
        Guid orderId,
        string cardNumber,
        string iban,
        string cvv,
        string holderName,
        string expirationDate,
        CancellationToken cancellationToken);
}