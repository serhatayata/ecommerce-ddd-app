namespace Shipping.Application.Commands.Shipments.Create;

public class CreateShipmentResponse
{
    public CreateShipmentResponse(
    int id)
    {
        Id = id;
    }

    public int Id { get; set; }
}