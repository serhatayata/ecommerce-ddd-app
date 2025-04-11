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
    // Private parameterless constructor for EF Core
    private Location() { }

    public Location(string warehouse, string aisle, string shelf)
    {
        if (string.IsNullOrWhiteSpace(warehouse))
            throw new ArgumentException("Warehouse cannot be empty", nameof(warehouse));

        Warehouse = warehouse;
        Aisle = aisle;
        Shelf = shelf;
    }

    // Properties must be protected set for EF Core
    public string Warehouse { get; protected set; }
    public string Aisle { get; protected set; }
    public string Shelf { get; protected set; }
    public string Bin { get; protected set; }

    public static Location Create(string warehouse, string aisle, string shelf, string bin)
        => new Location { Warehouse = warehouse, Aisle = aisle, Shelf = shelf, Bin = bin };

    public override string ToString() 
        => $"{Warehouse}-{Aisle}-{Shelf}-{Bin}";
}
