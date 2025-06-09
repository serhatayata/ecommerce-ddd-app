using Common.Application.Models.Responses.PaymentSystems;
using Common.Domain.ValueObjects;

namespace OrderManagement.Application.Services.PaymentSystems;

public interface IPaymentSystemApiService
{
    public Task<PaymentInfoCreateResponse> CreatePaymentInfoAsync(
        int orderId,
        string cardNumber,
        string iban,
        string cvv,
        string holderName,
        string expirationDate,
        CancellationToken cancellationToken);
}