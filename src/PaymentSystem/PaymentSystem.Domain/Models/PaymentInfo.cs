using Common.Domain.Models;

namespace PaymentSystem.Domain.Models;

/// <summary>
/// This class is used as a demo repository for keeping users' payment information.
/// ⚠️ In real-world applications, sensitive payment data such as card numbers, IBAN, and CVV
/// should NOT be stored in your own database due to security and compliance requirements (e.g., PCI DSS).
/// Instead, use a payment provider and store only tokens or references.
/// </summary>
public class PaymentInfo : Entity
{
    public PaymentInfo() { }

    public PaymentInfo(
    int orderId,
    string cardNumber,
    string iban,
    string cvv,
    string holderName)
    {
        OrderId = orderId;
        CardNumber = cardNumber;
        IBAN = iban;
        CVV = cvv;
        HolderName = holderName;
        CreatedAt = DateTime.UtcNow;
    }

    public int OrderId { get; private set; }
    public string CardNumber { get; private set; }
    public string IBAN { get; private set; }
    public string CVV { get; private set; }
    public string HolderName { get; private set; }
    public DateTime CreatedAt { get; private set; }
}