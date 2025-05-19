namespace PaymentSystem.Application.Commands.PaymentInformation;

public class PaymentInfoCreateResponse
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public string CardNumber { get; set; }
    public string IBAN { get; set; }
    public string CVV { get; set; }
    public string HolderName { get; set; }
    public DateTime CreatedAt { get; set; }
}