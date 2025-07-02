using Asp.Versioning;
using Boardly.Aplicacion.DTOs.Ceo;
using Boardly.Aplicacion.DTOs.Empleado;
using Boardly.Aplicacion.DTOs.Usuario;
using Boardly.Dominio.Puertos.CasosDeUso.Ceo;
using Boardly.Dominio.Puertos.CasosDeUso.Codigo;
using Boardly.Dominio.Puertos.CasosDeUso.Empleado;
using Boardly.Dominio.Puertos.CasosDeUso.Usuario;
using Boardly.Dominio.Utilidades;
using Microsoft.AspNetCore.Mvc;

namespace Boardly.Infraestructura.Api.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/user")]
public class UsuarioControlador(
    ICrearUsuario<CrearUsuarioDto, UsuarioDto> crearUsuario,
    ICrearCeo<CrearCeoDto, CeoDto> crearCeo,
    ICrearEmpleado<CrearEmpleadoDto, EmpleadoDto> crearEmpleado,
    IConfirmarCuenta<Resultado> confirmarCuenta
) : ControllerBase
{

    [HttpPost]
    public async Task<ActionResult<UsuarioDto>> CrearUsuario([FromForm] CrearUsuarioDto usuario, CancellationToken cancellationToken)
    {
        var resultado = await crearUsuario.CrearUsuarioAsync(usuario, cancellationToken);
        if(resultado.EsExitoso)
            return Ok(resultado.Valor);

        return BadRequest(resultado.Error);
    }
    
    [HttpPost("ceo")]
    public async Task<IActionResult> RegistrarCeo([FromQuery] CrearCeoDto solicitud, CancellationToken cancellationToken)
    {
        var resultado = await crearCeo.CrearCeoAsync(solicitud, cancellationToken);
        if (resultado.EsExitoso) 
            return Ok(resultado.Valor);
        
        return BadRequest(resultado.Error);
    }

    [HttpPost("empleado")]
    public async Task<IActionResult> CrearEmpleado([FromQuery] CrearEmpleadoDto solicitud,
        CancellationToken cancellationToken)
    {
        var empleado = await crearEmpleado.CrearEmpleadoAsync(solicitud, cancellationToken);
        if (empleado.EsExitoso)
        {
            return Ok(empleado.Valor);
        }
        return BadRequest(empleado.Error);
    }

    [HttpPost("confirm-account")]
    public async Task<IActionResult> ConfirmarCuentaAsync(
        [FromQuery] Guid usuarioId,
        [FromQuery] string codigo,
        CancellationToken cancellationToken
        )
    {
        var resultado = await confirmarCuenta.ConfirmarCuentaAsync(usuarioId, codigo, cancellationToken);
        if (resultado.EsExitoso)
            return Ok("Se ha confirmado su cuenta");
        
        return BadRequest(resultado.Error);
    }
    
}