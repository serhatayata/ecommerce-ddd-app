namespace Common.Domain.Models.DTOs.OrderManagements;

public class OrderItemDto
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}