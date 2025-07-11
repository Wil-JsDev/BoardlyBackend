using Asp.Versioning;
using Boardly.Aplicacion.DTOs.Paginacion;
using Boardly.Aplicacion.DTOs.RolProyecto;
using Boardly.Dominio.Puertos.CasosDeUso.RolProyecto;
using Microsoft.AspNetCore.Mvc;

namespace Boardly.Infraestructura.Api.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/roles-projects")]
public class RolProyectoController(
    ICrearRolProyecto<CrearRolProyectoDto, RolProyectoDto> crearRolProyecto,
    IBorrarRolProyecto borrarRolProyecto,
    IActualizarRolProyecto<CrearRolProyectoDto, string> actualizarRolProyecto,
    IObtenerIdRolProyecto<RolProyectoDto> obtenerRolProyecto,
    IResultadoPaginadoRolProyecto<PaginacionParametro, RolProyectoDto> paginacionRolProyecto
    ) : ControllerBase
{

    [HttpPost]
    public async Task<IActionResult> CrearRolProyecto([FromBody] CrearRolProyectoDto rolProyecto,
        CancellationToken cancellationToken)
    {
        var resultado = await crearRolProyecto.CrearRolProyectoAsync(rolProyecto, cancellationToken);
        if (resultado.EsExitoso)
            return Ok(resultado.Valor);
        
        return BadRequest(resultado.Error);
    }

    [HttpGet("{rolProjectId}")]
    public async Task<IActionResult> ObtenerIdRolProyectoAsync([FromRoute] Guid rolProjectId,
        CancellationToken cancellationToken)
    {
        var resultado = await obtenerRolProyecto.ObtenerIdRolProyectoAsync(rolProjectId, cancellationToken);
        if (resultado.EsExitoso)
            return Ok(resultado.Valor);
        
        return BadRequest(resultado.Error);
    }

    [HttpPut]
    public async Task<IActionResult> ActualizarRolProyectoAsync(
        [FromQuery] Guid rolProjectId,
        [FromQuery] Guid projectId,
        [FromBody] ParametroRolProyectoDto parametroRolProyecto,
        CancellationToken cancellationToken
    )
    {
        CrearRolProyectoDto rolProyecto = new(projectId, parametroRolProyecto.Nombre, parametroRolProyecto.Descripcion);
        var resultado = await actualizarRolProyecto.ActualizarRolProyectoAsync(rolProjectId, rolProyecto, cancellationToken);
        if (resultado.EsExitoso)
            return Ok(resultado.Valor);
        
        return NotFound(resultado.Error);
    }

    [HttpDelete("{rolProjectId}")]
    public async Task<IActionResult> BorrarRolProyectoAsync([FromRoute] Guid rolProjectId, CancellationToken cancellationToken)
    {
        var resultado = await borrarRolProyecto.BorrarRolProyectoAsync(rolProjectId, cancellationToken);
        if (resultado.EsExitoso)
            return Ok(resultado.Valor);
        
        return NotFound(resultado.Error);
    }

    [HttpGet("pagination/{projectId}")]
    public async Task<IActionResult> ObtenerPaginasRolProyectoAsync(
        [FromRoute] Guid projectId,
        [FromQuery] int numeroPagina,
        [FromQuery] int tamanoPagina,
        CancellationToken cancellationToken
        )
    {
        PaginacionParametro paginacion = new(numeroPagina, tamanoPagina);
        var resultado = await paginacionRolProyecto.ObtenerPaginacionRolProyectoAsync(projectId, paginacion, cancellationToken);
        if (resultado.EsExitoso)
            return Ok(resultado.Valor);
        
        return BadRequest(resultado.Error);
    }
    
}