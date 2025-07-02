using Asp.Versioning;
using Boardly.Aplicacion.DTOs.Autenticacion;
using Boardly.Dominio.Puertos.CasosDeUso.Autenticacion;
using Boardly.Dominio.Utilidades;
using Microsoft.AspNetCore.Mvc;

namespace Boardly.Infraestructura.Api.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/auth")]
public class AutenticacionControlador(
    IAutenticacion<AutenticacionRespuesta, AutenticacionSolicitud> autenticacion
) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Autenticarse([FromBody] AutenticacionSolicitud solicitud, CancellationToken cancellationToken)
    {
     var resultado = await autenticacion.AutenticarAsync(solicitud, cancellationToken);
     if (resultado.EsExitoso)
         return Ok(resultado.Valor);
     
        return BadRequest(resultado.Error);
    }
}