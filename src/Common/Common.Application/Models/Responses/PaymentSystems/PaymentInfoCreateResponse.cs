using Common.Domain.ValueObjects;

namespace Common.Application.Models.Responses.PaymentSystems;

public class PaymentInfoCreateResponse
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public string CardNumber { get; set; }
    public string IBAN { get; set; }
    public string CVV { get; set; }
    public string HolderName { get; set; }
    public string ExpirationDate { get; set; }
    public PaymentMethod Method { get; set; }
    public DateTime CreatedAt { get; set; }
}