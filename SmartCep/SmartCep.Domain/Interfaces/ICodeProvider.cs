using SmartCep.Domain.Entities;

namespace SmartCep.Domain.Interfaces;

public interface ICodeProvider
{
    Task<Code?> SearchAsync(string PostalCode);
}