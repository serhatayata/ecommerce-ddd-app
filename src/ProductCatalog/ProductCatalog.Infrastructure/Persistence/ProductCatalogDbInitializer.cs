using Common.Infrastructure.Persistence;
using ProductCatalog.Domain.Models.Products;
using ProductCatalog.Domain.Models.Suppliers;
using ProductCatalog.Domain.Models.Categories;
using ProductCatalog.Domain.Models.Brands;

namespace ProductCatalog.Infrastructure.Persistence;

internal class ProductCatalogDbInitializer : DbInitializer
{
    private readonly ProductCatalogDbContext _dbContext;

    public ProductCatalogDbInitializer(
    ProductCatalogDbContext dbContext)
    : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Initialize()
    {
        InitializeBrands();
        InitializeCategories();
        InitializeSuppliers();
        InitializeProducts();
    }

    private void InitializeBrands()
    {
        if (_dbContext.Brands.Any())
            return;

        var brands = new[]
        {
            new Brand("Dell", "Computer manufacturer", "www.dell.com"),
            new Brand("Philips", "Electronics manufacturer", "www.philips.com"),
            new Brand("Nike", "Sports equipment manufacturer", "www.nike.com"),
            new Brand("Sony", "Electronics and entertainment company", "www.sony.com")
        };

        _dbContext.Brands.AddRange(brands);
        _dbContext.SaveChanges();
    }

    private void InitializeCategories()
    {
        if (_dbContext.Categories.Any())
            return;

        var electronics = new Category("Electronics", "Electronic devices and accessories");
        var sports = new Category("Sports", "Sports equipment and accessories");
        var homeAppliances = new Category("Home Appliances", "Kitchen and home electronics");

        _dbContext.Categories.AddRange(electronics, sports, homeAppliances);
        _dbContext.SaveChanges();

        var children = new[]
        {
            new Category("Computers", "Desktop and laptop computers", electronics.Id),
            new Category("Audio Devices", "Headphones and speakers", electronics.Id),
            new Category("Footwear", "Athletic shoes and boots", sports.Id)
        };

        _dbContext.Categories.AddRange(children);
        _dbContext.SaveChanges();
    }

    private void InitializeSuppliers()
    {
        if (_dbContext.Suppliers.Any())
            return;

        var suppliers = new[]
        {
            new Supplier(
                "TechWorld Distribution",
                "John Smith",
                "john.smith@techworld.com",
                "1-555-0123",
                Address.From("123 Tech Street", "Silicon Valley", "CA", "USA", "94025")),
            
            new Supplier(
                "Global Electronics",
                "Jane Doe",
                "jane.doe@globalelec.com",
                "1-555-0124",
                Address.From("456 Electronics Ave", "New York", "NY", "USA", "10001"))
        };

        _dbContext.Suppliers.AddRange(suppliers);
        _dbContext.SaveChanges();
    }

    private void InitializeProducts()
    {
        if (_dbContext.Products.Any())
            return;

        var brand = _dbContext.Brands.First();
        var category = _dbContext.Categories.First();
        var supplier = _dbContext.Suppliers.First();

        var products = new[]
        {
            Product.Create(
                "Gaming Laptop",
                "High-performance gaming laptop with RTX 3080",
                1499.99m,
                brand.Id,
                category.Id,
                supplier.Id),
                
            Product.Create(
                "Coffee Maker",
                "Professional grade coffee maker with built-in grinder",
                299.99m,
                brand.Id,
                category.Id,
                supplier.Id),
                
            Product.Create(
                "Running Shoes",
                "Lightweight running shoes with extra cushioning",
                89.99m,
                brand.Id,
                category.Id,
                supplier.Id),
                
            Product.Create(
                "Wireless Headphones",
                "Noise-cancelling Bluetooth headphones",
                199.99m,
                brand.Id,
                category.Id,
                supplier.Id)
        };

        _dbContext.Products.AddRange(products);
        _dbContext.SaveChanges();
    }
}