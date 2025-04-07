using Common.Domain.Models;

namespace Stock.Domain.Models.Stocks;

/// <summary>
/// Represents a physical storage location in a warehouse.
/// This value object defines the hierarchical structure of storage locations
/// consisting of Warehouse, Aisle, Shelf, and Bin locations to precisely
/// identify where items are stored in the inventory system.
/// </summary>
public class Location : ValueObject
{
    public string Warehouse { get; }
    public string Aisle { get; }
    public string Shelf { get; }
    public string Bin { get; }

    private Location(string warehouse, string aisle, string shelf, string bin)
    {
        if (string.IsNullOrWhiteSpace(warehouse))
            throw new ArgumentException("Warehouse cannot be empty", nameof(warehouse));

        Warehouse = warehouse;
        Aisle = aisle;
        Shelf = shelf;
        Bin = bin;
    }

    public static Location Create(string warehouse, string aisle, string shelf, string bin)
        => new(warehouse, aisle, shelf, bin);

    public override string ToString() 
        => $"{Warehouse}-{Aisle}-{Shelf}-{Bin}";
}
