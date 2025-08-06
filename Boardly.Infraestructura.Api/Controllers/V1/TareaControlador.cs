using Boardly.Aplicacion.DTOs.Paginacion;
using Boardly.Aplicacion.DTOs.Tarea;
using Boardly.Dominio.Puertos.CasosDeUso.Tarea;
using Microsoft.AspNetCore.Mvc;

namespace Boardly.Infraestructura.Api.Controllers.V1;

[ApiController]
[Route("api/v{version:apiVersion}/tasks")]
public class TareaControlador(
    ICrearTarea<CrearTareaDto, TareaDto> crearTarea,
    IResultadoPaginadoTarea<PaginacionParametro, TareaDto> tareaPagina,
    IObtenerIdTarea<TareaDetalles> obtenerTarea,
    IBorrarTarea borrarTarea,
    IActualizarTarea<ActualizarTituloTareaDto, TareaDto> actualizarTarea
    ) : ControllerBase
{

    [HttpPost]
    public async Task<IActionResult> CrearTarea([FromForm] CrearTareaDto tarea, CancellationToken cancellationToken)
    {
        var resultado = await crearTarea.CrearTareaAsync(tarea, cancellationToken);
        if (resultado.EsExitoso)
            return Ok(resultado.Valor);
        
        return BadRequest(resultado.Error);
    }

    [HttpGet("{taskId}/info")]
    public async Task<IActionResult> ObtenerIdTareaAsync([FromRoute] Guid taskId, CancellationToken cancellationToken)
    {
        var resultado = await obtenerTarea.ObtenerIdUsuarioAsync(taskId, cancellationToken);
        if (resultado.EsExitoso)
            return Ok(resultado.Valor);
        
        return NotFound(resultado.Error);
    }

    [HttpGet("pagination/{activityId}")]
    public async Task<IActionResult> ResultadoPaginaTareaAsync(
        [FromQuery] int numeroPagina,
        [FromQuery] int tamanoPagina,
        [FromRoute] Guid activityId,
        CancellationToken cancellationToken
    )
    {
        PaginacionParametro parametros = new(numeroPagina, tamanoPagina);
        var resultado = await tareaPagina.ObtenerPaginacionTareaAsync(activityId, parametros, cancellationToken);
        if (resultado.EsExitoso)
            return Ok(resultado.Valor);
        
        return BadRequest(resultado.Error);
    }
    
    [HttpPut("{taskId}")]
    public async Task<IActionResult> ActualizarTareaAsync(
        [FromRoute] Guid taskId,
        [FromBody] ActualizarTituloTareaDto tarea,
        CancellationToken cancellationToken
        )
    {
        var resultado = await actualizarTarea.ActualizarTareaAsync(taskId, tarea, cancellationToken);
        if (resultado.EsExitoso)
            return Ok(resultado.Valor);
        
        return NotFound(resultado.Error);
    }

    [HttpDelete("{taskId}")]
    public async Task<IActionResult> BorrarTareaAsync([FromRoute] Guid taskId, CancellationToken cancellationToken)
    {
        var resultado = await borrarTarea.BorrarTareaAsync(taskId, cancellationToken);
        if (resultado.EsExitoso)
            return Ok(resultado.Valor);
        
        return NotFound(resultado.Error);
    }
    
}