namespace ProductCatalog.Application.Commands.Products.Create;

public class CreateProductResponse
{
    public CreateProductResponse(
    int id)
    {
        Id = id;
    }

    public int Id { get; set; }
}