namespace OrderManagement.Application.Commands;

public sealed record OrderAddResponse
{
    public Guid Id { get; set; }
    public int PaymentInfoId { get; set; }
    public DateTime OrderDate { get; set; }
}