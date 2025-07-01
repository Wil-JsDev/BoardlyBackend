using Asp.Versioning;
using Boardly.Aplicacion.DTOs.Usuario;
using Boardly.Dominio.Puertos.CasosDeUso.Codigo;
using Boardly.Dominio.Puertos.CasosDeUso.Usuario;
using Boardly.Dominio.Utilidades;
using Microsoft.AspNetCore.Mvc;

namespace Boardly.Infraestructura.Api.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/user")]
public class UsuarioControlador(
    ICrearUsuario<CrearUsuarioDto, UsuarioDto> crearUsuario,
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