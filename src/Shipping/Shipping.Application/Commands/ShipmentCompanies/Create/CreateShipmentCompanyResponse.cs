namespace Shipping.Application.Commands.ShipmentCompanies.Create;

public class CreateShipmentCompanyResponse
{
    public CreateShipmentCompanyResponse(
    int id)
    {
        Id = id;
    }

    public int Id { get; set; }
}