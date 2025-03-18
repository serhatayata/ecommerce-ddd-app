using System.Text.Json.Serialization;

namespace OrderManagement.Domain.DTOs;

public sealed record OrderItemDto
{
    [JsonConstructor]
    public OrderItemDto()
    {
    }

    public OrderItemDto(
    int productId, 
    int quantity, 
    decimal unitPrice)
    {
        ProductId = productId;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }

    public int ProductId { get; init; }
    public int Quantity { get; init; }
    public decimal UnitPrice { get; init; }
}