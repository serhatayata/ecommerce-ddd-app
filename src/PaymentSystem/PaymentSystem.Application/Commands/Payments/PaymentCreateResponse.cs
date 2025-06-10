namespace PaymentSystem.Application.Commands.Payments;

public class PaymentCreateResponse
{
    public int PaymentId { get; set; }
    public Guid OrderId { get; set; }
}