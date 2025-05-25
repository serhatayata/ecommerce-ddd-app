using Common.Domain.Models;
using ProductCatalog.Domain.Models.Products;

namespace ProductCatalog.Domain.Models.Brands;

public class Brand : Entity
{
    private readonly List<Product> _products = new();

    private Brand() { }

    private Brand(
        string name,
        string description,
        string website)
    {
        Name = name;
        Description = description;
        Website = website;
        IsActive = true;
    }

    public static Brand Create(
        string name,
        string description,
        string website)
        => new Brand(name, description, website);

    public string Name { get; private set; }
    public string Description { get; private set; }
    public string Website { get; private set; }
    public bool IsActive { get; private set; }

    public virtual IReadOnlyCollection<Product> Products => _products.AsReadOnly();
}