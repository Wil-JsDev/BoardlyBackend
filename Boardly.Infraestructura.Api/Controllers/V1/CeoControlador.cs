using Asp.Versioning;
using Boardly.Aplicacion.DTOs.Ceo;
using Boardly.Dominio.Puertos.CasosDeUso.Ceo;
using Microsoft.AspNetCore.Mvc;

namespace Boardly.Infraestructura.Api.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/ceos")]
public class CeoControlador(
    ICrearCeo<CrearCeoDto, CeoDto> crearCeo,
    IObtenerIdCeo obtenerCeo
    ) : ControllerBase
{
    [HttpPost("{userId}")]
    public async Task<IActionResult> RegistrarCeo([FromRoute] Guid userId, CancellationToken cancellationToken)
    {
        CrearCeoDto ceoDto = new(UsuarioId: userId);
        
        var resultado = await crearCeo.CrearCeoAsync(ceoDto, cancellationToken);
        if (resultado.EsExitoso) 
            return Ok(resultado.Valor);
        
        return BadRequest(resultado.Error);
    }
    
    [HttpGet("{userId}")]
    public async Task<IActionResult> ObtenerIdCeo([FromRoute] Guid userId, CancellationToken cancellationToken)
    {
        var ceoId = await obtenerCeo.ObtenerIdCeoAsync(userId, cancellationToken);
        if(ceoId.EsExitoso)
            return Ok(ceoId.Valor);
        
        return BadRequest(ceoId.Error);
    }
}