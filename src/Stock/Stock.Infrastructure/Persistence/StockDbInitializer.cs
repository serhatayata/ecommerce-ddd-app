using Common.Infrastructure.Persistence;
using Stock.Domain.Models.Stocks;

namespace Stock.Infrastructure.Persistence;

public class StockDbInitializer : DbInitializer
{
    StockDbContext _dbContext;

    public StockDbInitializer(StockDbContext dbContext)
    : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Initialize()
    {
        if (_dbContext.StockItems.Any())
            return;

        var stockItems = new List<StockItem>
        {
            StockItem.Create(1, 100, Location.Create("Warehouse A", "Zone 1", "Shelf A1", "Bin-01")),
            StockItem.Create(2, 50, Location.Create("Warehouse A", "Zone 2", "Shelf B3", "Bin-02")),
            StockItem.Create(3, 75, Location.Create("Warehouse B", "Zone 1", "Shelf C2", "Bin-03"))
        };

        _dbContext.StockItems.AddRange(stockItems);
        _dbContext.SaveChanges();

        // Add some transactions
        stockItems[0].AddStock(20, "Initial stock receipt");
        stockItems[0].RemoveStock(5, "Customer order fulfillment");
        stockItems[1].AddStock(30, "Supplier delivery");
        stockItems[2].RemoveStock(10, "Damaged goods removal");

        // Add some reservations
        stockItems[0].ReserveStock(10, 1001);
        stockItems[1].ReserveStock(5, 1002);

        _dbContext.SaveChanges();
    }
}