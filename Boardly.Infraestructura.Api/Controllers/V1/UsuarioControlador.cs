using Asp.Versioning;
using Boardly.Aplicacion.DTOs.Ceo;
using Boardly.Aplicacion.DTOs.Contrasena;
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
[Route("api/v{version:apiVersion}/users")]
public class UsuarioControlador(
    ICrearUsuario<CrearUsuarioDto, UsuarioDto> crearUsuario,
    IConfirmarCuenta<Resultado> confirmarCuenta,
    IObtenerIdUsuario<UsuarioDto> obtenerUsuario,
    IOlvidarContrasenaUsuario olvidarContrasena,
    IModificarContrasenaUsuario<ModificarContrasenaUsuarioDto> modificarContrasena
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
    

    [HttpPost("confirm-account")]
    public async Task<IActionResult> ConfirmarCuentaAsync(
        [FromQuery] Guid userId,
        [FromQuery] string code,
        CancellationToken cancellationToken
        )
    {
        var resultado = await confirmarCuenta.ConfirmarCuentaAsync(userId, code, cancellationToken);
        if (resultado.EsExitoso)
            return Ok("Se ha confirmado su cuenta");
        
        return BadRequest(resultado.Error);
    }
    
    [HttpGet("{userId}")]
    public async Task<IActionResult> ObtenerUsuarioAsync( [FromRoute] Guid userId, CancellationToken cancellationToken)
    {
        var resultado = await obtenerUsuario.ObtenerIdUsarioAsync(userId, cancellationToken);
        if (resultado.EsExitoso)
            return Ok(resultado.Valor);
            
        return NotFound(resultado.Error);
    }

    [HttpPost("forgot-password")]
    public async Task<IActionResult> OlvidarContrasenaAsync( [FromQuery] Guid userId, CancellationToken cancellationToken)
    {
        var resultado = await olvidarContrasena.OlvidarContrasena(userId, cancellationToken);
        if (resultado.EsExitoso)
            return Ok(resultado.Valor);
            
        return NotFound(resultado.Error);
    }

    [HttpPost("modify-password")]
    public async Task<IActionResult> ModificarContrasenaAsync(
        [FromQuery] Guid userId,
        [FromBody] ParametroModificarContrasenaDto parametro,
        CancellationToken cancellationToken)
    {

        ModificarContrasenaUsuarioDto usuario = new(userId, parametro.Codigo!, parametro.Contrasena!,
            parametro.ConfirmacionDeContrsena!);
        
        var resultado = await modificarContrasena.ModificarContasenaAsync(usuario, cancellationToken);
        if (resultado.EsExitoso)
            return Ok(resultado.Valor);
            
        return BadRequest(resultado.Error);
    }
    
}