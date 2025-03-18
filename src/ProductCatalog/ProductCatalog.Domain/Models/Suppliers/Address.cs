using Common.Domain.Models;

namespace ProductCatalog.Domain.Models.Suppliers;

public class Address : ValueObject
{
    public string Street { get; }
    public string City { get; }
    public string State { get; }
    public string Country { get; }
    public string ZipCode { get; }

    private Address(
        string street,
        string city,
        string state,
        string country,
        string zipCode)
    {
        if (string.IsNullOrWhiteSpace(street))
            throw new ArgumentException("Street cannot be empty", nameof(street));
        if (string.IsNullOrWhiteSpace(city))
            throw new ArgumentException("City cannot be empty", nameof(city));
        if (string.IsNullOrWhiteSpace(country))
            throw new ArgumentException("Country cannot be empty", nameof(country));

        Street = street;
        City = city;
        State = state;
        Country = country;
        ZipCode = zipCode;
    }

    public static Address From(
        string street,
        string city,
        string state,
        string country,
        string zipCode)
    {
        return new Address(street, city, state, country, zipCode);
    }

    public override string ToString() 
        => $"{Street}, {City}, {State} {ZipCode}, {Country}";
}