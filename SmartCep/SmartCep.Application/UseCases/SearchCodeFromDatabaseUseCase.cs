using SmartCep.Application.DTOs;
using SmartCep.Domain.Interfaces;

namespace SmartCep.Application.UseCases;

public class SearchCodeFromDatabaseUseCase(ICodeRepository repository)
{
    public async Task<CodeDto?> ExecuteAsync(string postalCode)
    {
        postalCode = postalCode.Replace("-", "").Replace(".", "").Trim();

        if (postalCode.Length != 8)
            throw new ArgumentException("CEP inválido");

        var result = await repository.GetByPostalCodeAsync(postalCode);

        return result is null
            ? null
            : CodeDto.FromDomain(result);
    }
}

