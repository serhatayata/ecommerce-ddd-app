namespace Common.Domain.Events.Shippings;

public sealed record ShipmentDeliverFailedEvent : IntegrationEvent
{
    public ShipmentDeliverFailedEvent(
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