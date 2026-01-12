namespace SmartCep.Domain.Entities;

public class Code(string postalCode, string street, string neighborhood, string city, string state)
{
    public string PostalCode { get; } = postalCode;
    public string Street { get; } = street;
    public string Neighborhood { get; } = neighborhood;
    public string City { get; } = city;
    public string State { get; } = state;
}