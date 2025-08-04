using Asp.Versioning;
using Boardly.Aplicacion.Adaptadores.Empleados;
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
    IResultadoPaginadoEmpleadoPorProyectoId<PaginacionParametro, EmpleadoRolProyectoDto> empleadoPorProyecto,
    IObtenerEstadisticasEnConteoEmpleado<ConteoEmpleadoDto> estadisticasEnConteoEmpleado,
    IActualizarRolEmpleado<ActualizarRolEmpleadoDto, EmpleadoResumenDto> rolEmpleado,
    IResultadoPaginadoEmpleadoEmpresaId<PaginacionParametro, EmpleadoRolProyectoDto> paginacionEmpleadoEmpresaId,
    IBorrarEmpleadoProyecto borrarEmpleado,
    IAgregarEmpleadoProyecto<AgregarEmpleadoProyectoDto, EmpleadoResumenDto> empleadoProyecto) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CrearEmpleado([FromBody] CrearEmpleadoDto employee, CancellationToken cancellationToken)
    {
        var empleado = await crearEmpleado.CrearEmpleadoAsync(employee, cancellationToken);
        if (empleado.EsExitoso)
            return Ok(empleado.Valor);
             
        return BadRequest(empleado.Error);
    }
    
    [HttpPut("{employeeId}/role")]
    public async Task<IActionResult> CrearEmpleado([FromRoute] Guid employeeId,[FromBody] ActualizarRolEmpleadoDto rolEmpleadoDto ,CancellationToken cancellationToken)
    {
        var empleado = await rolEmpleado.ActualizarRolEmpleadoAsync(employeeId,rolEmpleadoDto, cancellationToken);
        if (empleado.EsExitoso)
            return Ok(empleado.Valor);
             
        return BadRequest(empleado.Error);
    }
    
    [HttpPut("{employeeId}")]
    public async Task<IActionResult> CrearEmpleado([FromRoute] Guid employeeId, [FromBody] AgregarEmpleadoProyectoDto empleadoProyectoDto  ,CancellationToken cancellationToken)
    {
        var empleado = await empleadoProyecto.AgregarEmpleadoProyectoAsync(employeeId,empleadoProyectoDto , cancellationToken);
        if (empleado.EsExitoso)
            return Ok(empleado.Valor);
             
        return BadRequest(empleado.Error);
    }
    
    [HttpDelete("{employeeId}/projects/{projectId}")]
    public async Task<IActionResult> RemoverEmpleadoDeProyecto([FromRoute] Guid employeeId, [FromRoute] Guid projectId ,CancellationToken cancellationToken)
    {
        var empleado = await borrarEmpleado.BorrarEmpleadoProyectoAsync(employeeId,projectId , cancellationToken);
        if (empleado.EsExitoso)
            return Ok(empleado.Valor);
             
        return BadRequest(empleado.Error);
    }
    
    [HttpGet("{employeeId}/count")]
    public async Task<IActionResult> ObtenerCountsPorEmpleadoIdAsync([FromRoute] Guid employeeId,
        CancellationToken cancellationToken)
    {
        var resultado = await estadisticasEnConteoEmpleado.ObtenerEstadisticasEnConteoEmpleadoAsync(employeeId, cancellationToken);
        if (resultado.EsExitoso)
            return Ok(resultado.Valor);
        
        return BadRequest(resultado.Error);
    }
    
    [HttpGet("by-company/{companyId}")]
    public async Task<IActionResult> ObtenerEmpleadosPorEmpresaId(
        [FromRoute] Guid companyId, 
        [FromQuery] int numeroPagina, 
        [FromQuery] int tamanoPagina, 
        CancellationToken cancellationToken
        )
    {
        PaginacionParametro parametros = new(numeroPagina, tamanoPagina);
        var resultado = await paginacionEmpleadoEmpresaId.ResultadoPaginadoEmpleadoEmpresaIdAsync(companyId,parametros, cancellationToken);
        if (resultado.EsExitoso)
            return Ok(resultado.Valor);
        
        return BadRequest(resultado.Error);
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