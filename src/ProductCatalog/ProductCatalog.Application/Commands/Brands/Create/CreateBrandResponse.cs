namespace ProductCatalog.Application.Commands.Brands.Create;

public class CreateBrandResponse
{
    public CreateBrandResponse(
    int id)
    {
        Id = id;
    }

    public int Id { get; set; }
}