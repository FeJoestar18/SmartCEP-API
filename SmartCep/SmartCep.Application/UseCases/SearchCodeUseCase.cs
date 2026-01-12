using SmartCep.Application.DTOs;
using SmartCep.Domain.Interfaces;

namespace SmartCep.Application.UseCases;

public class SearchCodeUseCase(ICodeProvider PostalCodeProvider)
{
    public async Task<CodeDto?> ExecuteAsync(string PostalCode)
    {
        PostalCode = PostalCode.Replace(".","").Trim();

        var PostalCodeEntity = await PostalCodeProvider.SearchAsync(PostalCode);
        return PostalCodeEntity == null ? null : CodeDto.FromDomain(PostalCodeEntity);
    }
}

