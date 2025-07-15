using Asp.Versioning;
using Boardly.Aplicacion.DTOs.Actividad;
using Boardly.Aplicacion.DTOs.Paginacion;
using Boardly.Dominio.Puertos.CasosDeUso.Actividad;
using Microsoft.AspNetCore.Mvc;

namespace Boardly.Infraestructura.Api.Controllers.V1;


[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/activities")]
public class ActividadController(
    ICrearActividad<CrearActividaDto, ActividadDto> crearActividad,
    IObtenerIdActividad<ActividadDto> obtenerActividad,
    IBorrarActividad borrarActividad,
    IActualizarActividad<ActualizarActividadDto, ActividadDto> actualizarActividad,
    IResultadoPaginadoActividad<PaginacionParametro, ActividadDto> resultadoPaginadoActividad
        ) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CrearActividadAsync([FromBody] CrearActividaDto actividad,
        CancellationToken cancellationToken)
    {
        var resultado = await crearActividad.CrearActividadAsync(actividad, cancellationToken);
        if (resultado.EsExitoso)
            return Ok(resultado.Valor);
        
        return BadRequest(resultado.Error);
    }

    [HttpGet("{activityId}")]
    public async Task<IActionResult> ObtenerActividadAsync([FromRoute] Guid activityId,
        CancellationToken cancellationToken)
    {
        var resultado = await obtenerActividad.ObtenerIdActividadAsync(activityId, cancellationToken);
        if (resultado.EsExitoso)
            return Ok(resultado.Valor);
        
        return NotFound(resultado.Error);
    }

    [HttpPut("{activityId}")]
    public async Task<IActionResult> ObtenerIdActividadAsync(
        [FromRoute] Guid activityId,
        [FromBody] ActualizarActividadDto actividad,
        CancellationToken cancellationToken
        )
    {
        var resultado = await actualizarActividad.ActualizarActividadAsync(activityId, actividad, cancellationToken);
        if (resultado.EsExitoso)
            return Ok(resultado.Valor);
        
        return NotFound(resultado.Error);
    }
    
    [HttpDelete("{activityId}")]
    public async Task<IActionResult> BorrarActividadAsync([FromRoute] Guid activityId,
        CancellationToken cancellationToken)
    {
        var resultado = await borrarActividad.BorrarActividadAsync(activityId, cancellationToken);
        if (resultado.EsExitoso)
            return Ok(resultado.Valor);
        
        return NotFound(resultado.Error);
    }
    
    [HttpGet("{projectId}/pagination")]
    public async Task<IActionResult> ResultadoPaginadoActividad(
        [FromRoute] Guid projectId,
        [FromQuery] int numeroPagina,
        [FromQuery] int tamanoPagina,
        CancellationToken cancellationToken
    )
    {
        PaginacionParametro parametros = new(numeroPagina, tamanoPagina);
        var resultado = await resultadoPaginadoActividad.ObtenerPaginacionActividadAsync(projectId,parametros, cancellationToken);
        if (resultado.EsExitoso)
            return Ok(resultado.Valor);
        
        return BadRequest(resultado.Error);
    }
    
}