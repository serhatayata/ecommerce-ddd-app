using Common.Domain.Events;

namespace Shipping.Domain.Events;

public sealed record ShipmentDeliverFailedDomainEvent : DomainEvent
{
    public ShipmentDeliverFailedDomainEvent(
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

    public int ShipmentId { get; set; }
    public DateTime CreationDate { get; set; }
    public string ErrorMessage { get; set; }
}
