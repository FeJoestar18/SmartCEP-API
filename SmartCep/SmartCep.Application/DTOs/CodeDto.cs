namespace SmartCep.Application.DTOs;

public record CodeDto(
    string Code,
    string Street,
    string Neighborhood,
    string City,
    string State)
{
    public static CodeDto FromDomain(Domain.Entities.Code code)
    {
        return new CodeDto(
            code.PostalCode,
            code.Street,
            code.Neighborhood,
            code.City,
            code.State);
    }
}