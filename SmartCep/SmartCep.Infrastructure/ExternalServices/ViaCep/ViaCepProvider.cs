using SmartCep.Domain.Entities;
using SmartCep.Domain.Interfaces;
using System.Net.Http.Json;

namespace SmartCep.Infrastructure.ExternalServices.ViaCep;

public class ViaCepProvider(HttpClient httpClient) : ICodeProvider
{
    public async Task<Code?> SearchAsync(string code)
    {
        try
        {
            code = code?.Replace(".", "").Replace("-", "").Trim() ?? string.Empty;

            var httpResponse = await httpClient.GetAsync($"https://viacep.com.br/ws/{code}/json/");

            if (!httpResponse.IsSuccessStatusCode)
                return null;

            var response = await httpResponse.Content.ReadFromJsonAsync<ViaCepResponse>();

            if (response == null || response.Error)
                return null;

            return new Code(
                response.Postalcode,
                response.Street,
                response.Neighborhood,
                response.City,
                response.State
            );
        }
        catch (HttpRequestException)
        {
            return null;
        }
    }

}