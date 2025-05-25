using Common.Domain.Models;
using Common.Domain.ValueObjects;

namespace PaymentSystem.Domain.Models;

/// <summary>
/// This class is used as a demo repository for keeping users' payment information.
/// ⚠️ In real-world applications, sensitive payment data such as card numbers, IBAN, and CVV
/// should NOT be stored in your own database due to security and compliance requirements (e.g., PCI DSS).
/// Instead, use a payment provider and store only tokens or references.
/// </summary>
public class PaymentInfo : Entity
{
    private PaymentInfo() { }

    private PaymentInfo(
    OrderId orderId,
    string cardNumber,
    string iban,
    string cvv,
    string holderName,
    string expirationDate, // Add this
    PaymentMethod method)
    {
        OrderId = orderId;
        CardNumber = cardNumber;
        IBAN = iban;
        CVV = cvv;
        HolderName = holderName;
        ExpirationDate = expirationDate; // Add this
        Method = method;
        CreatedAt = DateTime.UtcNow;
    }

    public static PaymentInfo Create(
        OrderId orderId,
        string cardNumber,
        string iban,
        string cvv,
        string holderName,
        string expirationDate, // Add this
        PaymentMethod method)
        => new PaymentInfo(orderId, cardNumber, iban, cvv, holderName, expirationDate, method);

    public OrderId OrderId { get; private set; }
    public string CardNumber { get; private set; }
    public string IBAN { get; private set; }
    public string CVV { get; private set; }
    public string HolderName { get; private set; }
    public string ExpirationDate { get; private set; }
    public PaymentMethod Method { get; private set; }
    public DateTime CreatedAt { get; private set; }
}