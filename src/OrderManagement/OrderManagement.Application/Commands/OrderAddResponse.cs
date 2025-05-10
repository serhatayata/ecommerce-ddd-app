namespace OrderManagement.Application.Commands;

public sealed record OrderAddResponse
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; }
}