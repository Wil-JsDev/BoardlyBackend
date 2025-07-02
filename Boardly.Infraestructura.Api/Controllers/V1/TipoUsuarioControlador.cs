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
    [HttpPost("ceo/{usuarioId:guid}")]
    public async Task<IActionResult> RegistrarCeo([FromRoute] Guid usuarioId, CancellationToken cancellationToken)
    {
        CrearCeoDto ceoDto = new CrearCeoDto(UsuarioId: usuarioId);
        
        var resultado = await crearCeo.CrearCeoAsync(ceoDto, cancellationToken);
        if (resultado.EsExitoso) 
            return Ok(resultado.Valor);
        
        return BadRequest(resultado.Error);
    }

    [HttpPost("employee/{usuarioId:guid}")]
    public async Task<IActionResult> CrearEmpleado([FromQuery] Guid usuarioId, CancellationToken cancellationToken)
    {
        CrearEmpleadoDto empleadoDto = new(UsuarioId: usuarioId);
        
        var empleado = await crearEmpleado.CrearEmpleadoAsync(empleadoDto, cancellationToken);
        if (empleado.EsExitoso)
        {
            return Ok(empleado.Valor);
        }
        return BadRequest(empleado.Error);
    }
}