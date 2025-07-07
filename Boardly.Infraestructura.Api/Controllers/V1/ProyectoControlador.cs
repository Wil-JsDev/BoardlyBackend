using Asp.Versioning;
using Boardly.Aplicacion.DTOs.Paginacion;
using Boardly.Aplicacion.DTOs.Proyecto;
using Boardly.Dominio.Puertos.CasosDeUso.Proyecto;
using Microsoft.AspNetCore.Mvc;

namespace Boardly.Infraestructura.Api.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/projects")]
public class ProyectoControlador(
    ICrearProyecto<CrearProyectoDto, ProyectoDto> crearProyecto,
    IActualizarProyecto<ActualizarProyectoDto, ActualizarProyectoDto> actualizarProyecto,
    IObtenerIdProyecto<ProyectoDto> obtenerProyecto,
    IBorrarProyecto borrarProyecto,
    IResultadoPaginaProyecto<PaginacionParametro, ProyectoDto> resultadoPaginaProyecto
    ) : ControllerBase
{

    [HttpPost]
    public async Task<IActionResult> CrearProyecto([FromBody] CrearProyectoDto proyecto,
        CancellationToken cancellationToken)
    {
        var resultado = await crearProyecto.CrearProyectoAsync(proyecto, cancellationToken);
        if (resultado.EsExitoso)
            return Ok(resultado.Valor);
        
        return BadRequest(resultado.Error);
    }

    [HttpGet("{companyId}/pagination")]
    public async Task<IActionResult> ResultadoPaginaProyecto(
        [FromRoute] Guid companyId,
        [FromQuery] int numeroPagina,
        [FromQuery] int tamanoPagina,
        CancellationToken cancellationToken
    )
    {
        PaginacionParametro parametros = new(numeroPagina, tamanoPagina);
        var resultado = await resultadoPaginaProyecto.ObtenerPaginacionProyectoAsync(companyId, parametros, cancellationToken);
        if (resultado.EsExitoso)
            return Ok(resultado.Valor);
        
        return BadRequest(resultado.Error);
    }

    [HttpPut("{projectId}")]
    public async Task<IActionResult> ActualizarProyectoAsync( 
        [FromRoute] Guid projectId,
        [FromBody] ActualizarProyectoDto proyecto,
        CancellationToken cancellationToken
        )
    {
        var resultado = await actualizarProyecto.ActualizarProyectoAsync(projectId, proyecto , cancellationToken);
        if (resultado.EsExitoso)
            return Ok(resultado.Valor);
        
        return BadRequest(resultado.Error);
    }

    [HttpGet("{projectId}")]
    public async Task<IActionResult> ObtenerProyectoAsync([FromRoute] Guid projectId,
        CancellationToken cancellationToken)
    {
        var resultado = await obtenerProyecto.ObtenerIdProyectoAsync(projectId, cancellationToken);
        if (resultado.EsExitoso)
            return Ok(resultado.Valor);
        
        return BadRequest(resultado.Error);
    }
    
    [HttpDelete("{projectId}")]
    public async Task<IActionResult> BorrarProyectoAsync([FromRoute] Guid projectId,
        CancellationToken cancellationToken)
    {
        var resultado = await borrarProyecto.BorrarProyectoAsync(projectId, cancellationToken);
        if (resultado.EsExitoso)
            return NoContent();
        
        return BadRequest(resultado.Error);
    }
    
}