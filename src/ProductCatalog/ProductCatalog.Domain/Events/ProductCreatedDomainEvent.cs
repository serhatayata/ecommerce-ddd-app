using System.Text.Json.Serialization;
using Common.Domain.Events;

namespace ProductCatalog.Domain.Events;

public sealed record ProductCreatedDomainEvent : DomainEvent
{
    [JsonConstructor]
    public ProductCreatedDomainEvent()
    {
        
    }

    public ProductCreatedDomainEvent(
    int productId, 
    string productName,
    Guid? correlationId = null)
    : base(correlationId)
    {
        ProductId = productId;
        ProductName = productName;
    }

    public int ProductId { get; }
    public string ProductName { get; }
}