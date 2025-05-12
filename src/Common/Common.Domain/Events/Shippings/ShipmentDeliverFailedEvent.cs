namespace Common.Domain.Events.Shippings;

public sealed record ShipmentDeliverFailedEvent : IntegrationEvent
{
    public ShipmentDeliverFailedEvent(
        Guid? correlationId,
        int shipmentId,
        string trackingNumber,
        DateTime creationDate,
        string errorMessage)
        : base(correlationId, DateTime.UtcNow)
    {
        ShipmentId = shipmentId;
        TrackingNumber = trackingNumber;
        CreationDate = creationDate;
        ErrorMessage = errorMessage;
    }

    public int? ShipmentId { get; }
    public string TrackingNumber { get; }
    public DateTime CreationDate { get; }
    public string ErrorMessage { get; }
}