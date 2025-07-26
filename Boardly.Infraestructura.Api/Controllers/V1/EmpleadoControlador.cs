using Asp.Versioning;
using Boardly.Aplicacion.DTOs.Empleado;
using Boardly.Aplicacion.DTOs.Paginacion;
using Boardly.Dominio.Puertos.CasosDeUso.Empleado;
using Microsoft.AspNetCore.Mvc;

namespace Boardly.Infraestructura.Api.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/employee")]
public class EmpleadoControlador(
    ICrearEmpleado<CrearEmpleadoDto, EmpleadoDto> crearEmpleado,
    IObtenerEmpleadoPorEmpresaId<EmpleadoResumenDto>obtenerEmpleado,
    IResultadoPaginadoEmpleadoPorProyectoId<PaginacionParametro, EmpleadoRolProyectoDto> empleadoPorProyecto
    ) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CrearEmpleado([FromBody] CrearEmpleadoDto employee, CancellationToken cancellationToken)
    {
        var empleado = await crearEmpleado.CrearEmpleadoAsync(employee, cancellationToken);
        if (empleado.EsExitoso)
            return Ok(empleado.Valor);
             
        return BadRequest(empleado.Error);
    }

    [HttpGet("companies/{companyId}")]
    public async Task<IActionResult> ObtenerIdCeo([FromRoute] Guid companyId, CancellationToken cancellationToken)
    {
        var resultado = await obtenerEmpleado.ObtenerEmpleadoPorEmpresaIdAsync(companyId, cancellationToken);
        if (resultado.EsExitoso)
            return Ok(resultado.Valor);
        
        return BadRequest(resultado.Error);
    }

    [HttpGet("{projectId}/pagination")]
    public async Task<IActionResult> ResultadoEmpleadoPorProyectoIdAsync(
        [FromRoute] Guid projectId,
        [FromQuery] int numeroPagina,
        [FromQuery] int tamanoPagina,
        CancellationToken cancellationToken
    )
    {
        PaginacionParametro parametros = new(numeroPagina, tamanoPagina);
        var resultado = await empleadoPorProyecto.ObtenerPaginacionEmpleadoPorProyectoIdAsync(projectId, parametros, cancellationToken);
        if (resultado.EsExitoso)
            return Ok(resultado.Valor);
        
        return BadRequest(resultado.Error);;
    }
    
}