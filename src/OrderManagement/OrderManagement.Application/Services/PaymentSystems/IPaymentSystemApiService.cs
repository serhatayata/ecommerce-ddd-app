namespace OrderManagement.Application.Services.PaymentSystems;

public interface IPaymentSystemApiService
{
    public Task<int> CreatePaymentInfoAsync(int orderId, string cardNumber, string iban, string cvv, string holderName, CancellationToken cancellationToken);
}