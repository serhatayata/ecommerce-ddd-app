namespace ProductCatalog.Application.Commands.Categories.Create;

public class CreateCategoryResponse
{
    public CreateCategoryResponse(
    int id)
    {
        Id = id;
    }

    public int Id { get; set; }
}