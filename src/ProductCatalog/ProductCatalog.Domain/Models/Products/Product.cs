using Common.Domain.Models;
using Common.Domain.ValueObjects;
using ProductCatalog.Domain.Models.Brands;
using ProductCatalog.Domain.Models.Categories;
using ProductCatalog.Domain.Models.Suppliers;

namespace ProductCatalog.Domain.Models.Products;

public class Product : Entity, IAggregateRoot
{
    private Product(
        string name,
        string description,
        Money price,
        int brandId,
        int categoryId,
        int supplierId)
    {
        Name = name;
        Description = description;
        Price = price;
        BrandId = brandId;
        CategoryId = categoryId;
        SupplierId = supplierId;
        Status = ProductStatus.Draft;
        CreatedAt = DateTime.UtcNow;
    }

    public string Name { get; private set; }
    public string Description { get; private set; }
    public Money Price { get; private set; }
    public int BrandId { get; private set; }
    public int CategoryId { get; private set; }
    public int SupplierId { get; private set; }
    public ProductStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    public virtual Brand Brand { get; private set; }
    public virtual Category Category { get; private set; }
    public virtual Supplier Supplier { get; private set; }

    public static Product Create(
    string name,
    string description,
    Money price,
    int brandId,
    int categoryId,
    int supplierId)
        => new Product(
            name,
            description,
            price,
            brandId,
            categoryId,
            supplierId);

    public Product Update(
        string name,
        string description,
        decimal price,
        int brandId,
        int categoryId,
        int supplierId)
    {
        var moneyResult = Money.From(price);

        Name = name;
        Description = description;
        Price = moneyResult;
        BrandId = brandId;
        CategoryId = categoryId;
        SupplierId = supplierId;
        UpdatedAt = DateTime.UtcNow;

        return this;
    }
}