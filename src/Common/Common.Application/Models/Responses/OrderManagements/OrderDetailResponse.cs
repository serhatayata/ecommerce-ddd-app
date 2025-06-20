using Common.Domain.Models.DTOs.OrderManagements;
using Common.Domain.ValueObjects;

namespace Common.Application.Models.Responses.OrderManagements;

public class OrderDetailResponse
{
    public Guid Id { get; set; }
    public int UserId { get; set; }
    public DateTime OrderDate { get; set; }
    public int Status { get; set; }

    public HashSet<OrderItemDto> OrderItems { get; set; }
}