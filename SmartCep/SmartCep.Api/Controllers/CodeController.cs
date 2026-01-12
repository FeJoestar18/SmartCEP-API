using Microsoft.AspNetCore.Mvc;
using SmartCep.Application.UseCases;

namespace SmartCep.Api.Controllers;

[ApiController]
[Route("api/cep")]
public class CepController(SearchCodeFromDatabaseUseCase useCase) : ControllerBase
{
    [HttpGet("{cep}")]
    public async Task<IActionResult> Get([FromRoute] string cep)
    {
        try
        {
            var resultado = await useCase.ExecuteAsync(cep);

            if (resultado is null)
                return NotFound(new { message = "CEP não Encontrado" });

            return Ok(resultado);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}