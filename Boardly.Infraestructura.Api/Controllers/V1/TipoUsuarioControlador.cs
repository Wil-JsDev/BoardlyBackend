using Asp.Versioning;
using Boardly.Aplicacion.DTOs.Ceo;
using Boardly.Aplicacion.DTOs.Empleado;
using Boardly.Dominio.Puertos.CasosDeUso.Ceo;
using Boardly.Dominio.Puertos.CasosDeUso.Empleado;
using Microsoft.AspNetCore.Mvc;

namespace Boardly.Infraestructura.Api.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/user-type")]
public class TipoUsuarioControlador(
    ICrearCeo<CrearCeoDto, CeoDto> crearCeo,
    ICrearEmpleado<CrearEmpleadoDto, EmpleadoDto> crearEmpleado
    ) : ControllerBase
{
    [HttpPost("ceo/{userId}")]
    public async Task<IActionResult> RegistrarCeo([FromRoute] Guid userId, CancellationToken cancellationToken)
    {
        CrearCeoDto ceoDto = new(UsuarioId: userId);
        
        var resultado = await crearCeo.CrearCeoAsync(ceoDto, cancellationToken);
        if (resultado.EsExitoso) 
            return Ok(resultado.Valor);
        
        return BadRequest(resultado.Error);
    }

    [HttpPost("employee/{userId}")]
    public async Task<IActionResult> CrearEmpleado([FromRoute] Guid userId, CancellationToken cancellationToken)
    {
        CrearEmpleadoDto empleadoDto = new(UsuarioId: userId);
        
        var empleado = await crearEmpleado.CrearEmpleadoAsync(empleadoDto, cancellationToken);
        if (empleado.EsExitoso)
            return Ok(empleado.Valor);
             
        return BadRequest(empleado.Error);
    }
}