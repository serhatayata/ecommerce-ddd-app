using Common.Domain.Events;

namespace Shipping.Domain.Events;

public sealed record ShipmentShipFailedDomainEvent : DomainEvent
{
    public ShipmentShipFailedDomainEvent(
        int shipmentId,
        DateTime creationDate,
        string errorMessage,
        Guid? correlationId)
        : base(correlationId)
    {
        ShipmentId = shipmentId;
        CreationDate = creationDate;
        ErrorMessage = errorMessage;
    }

    public int ShipmentId { get; }
    public DateTime CreationDate { get; }
    public string ErrorMessage { get; }
}