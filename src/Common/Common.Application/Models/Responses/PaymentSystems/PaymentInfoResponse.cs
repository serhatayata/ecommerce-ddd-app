namespace Common.Application.Models.Responses.PaymentSystems;

public sealed record PaymentInfoResponse
{
    public int Id { get; set; }
    public int OrderId { get; private set; }
    public string CardNumber { get; private set; }
    public string IBAN { get; private set; }
    public string CVV { get; private set; }
    public string HolderName { get; private set; }
    public DateTime CreatedAt { get; private set; }
}