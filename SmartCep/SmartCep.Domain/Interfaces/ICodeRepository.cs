using SmartCep.Domain.Entities;

namespace SmartCep.Domain.Interfaces;

public interface ICodeRepository
{
    Task<Code?> GetByPostalCodeAsync(string postalCode);
    Task SaveAsync(Code code);
}

