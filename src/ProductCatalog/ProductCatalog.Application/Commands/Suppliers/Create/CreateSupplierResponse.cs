namespace ProductCatalog.Application.Commands.Suppliers.Create;

public class CreateSupplierResponse
{
    public int Id { get; set; }

    public CreateSupplierResponse(int id)
    {
        Id = id;
    }
}