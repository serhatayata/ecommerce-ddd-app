using Common.Domain.Models;
using ProductCatalog.Domain.Models.Products;

namespace ProductCatalog.Domain.Models.Categories;

public class Category : Entity
{
    private readonly List<Product> _products = new();

    public Category(
        string name,
        string description,
        int? parentId = null)
    {
        Name = name;
        Description = description;
        ParentId = parentId;
        IsActive = true;
    }

    public string Name { get; private set; }
    public string Description { get; private set; }
    public int? ParentId { get; private set; }
    public bool IsActive { get; private set; }

    public virtual Category? Parent { get; private set; }
    public virtual IReadOnlyCollection<Category> Children { get; private set; }
    public virtual IReadOnlyCollection<Product> Products => _products.AsReadOnly();
}