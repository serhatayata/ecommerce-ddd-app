namespace Common.Domain.Events.Shippings;

public sealed record ShipmentShipFailedEvent : IntegrationEvent
{
    public ShipmentShipFailedEvent(
        Guid? correlationId,
        int shipmentId,
        DateTime creationDate,
        string errorMessage)
        : base(correlationId, DateTime.UtcNow)
    {
        ShipmentId = shipmentId;
        CreationDate = creationDate;
        ErrorMessage = errorMessage;
    }

    public int? ShipmentId { get; }
    public DateTime CreationDate { get; }
    public string ErrorMessage { get; }
}