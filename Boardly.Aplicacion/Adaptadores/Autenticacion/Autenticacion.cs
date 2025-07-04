using Boardly.Aplicacion.DTOs.Autenticacion;
using Boardly.Aplicacion.DTOs.JWT;
using Boardly.Dominio.Puertos.CasosDeUso.Autenticacion;
using Boardly.Dominio.Puertos.Repositorios.Cuentas;
using Boardly.Dominio.Utilidades;
using Microsoft.Extensions.Logging;

namespace Boardly.Aplicacion.Adaptadores.Autenticacion;

public class Autenticacion(
    ILogger<Autenticacion> logger,
    IUsuarioRepositorio usuarioRepositorio,
    IGenerarToken<Dominio.Modelos.Usuario> token,
    ITokenRefrescado<TokenRefrescadoDto> tokenRefrescado
    ): IAutenticacion<AutenticacionRespuesta, AutenticacionSolicitud>
{
    public async Task<ResultadoT<AutenticacionRespuesta>> AutenticarAsync(AutenticacionSolicitud solicitud, CancellationToken cancellationToken)
    {
        
        AutenticacionRespuesta respuesta = new();
        
        var usuario = await usuarioRepositorio.BuscarPorEmailUsuarioAsync(solicitud.Correo, cancellationToken);
        if (usuario is null)
        {
            return ResultadoT<AutenticacionRespuesta>.Fallo(Error.NoEncontrado("404", "No se encontró ninguna cuenta asociada a ese correo electrónico"));
        }

        var esValido = BCrypt.Net.BCrypt.Verify(solicitud.Contrasena, usuario.Contrasena);
        if (!esValido)
        {
            return ResultadoT<AutenticacionRespuesta>.Fallo(Error.Fallo("401", "La contraseña es incorrecta"));

        }

        if (!usuario.CuentaConfirmada)
        {
            logger.LogWarning("La cuenta no esta confirmada. Usuario: {UsuarioId}", usuario.UsuarioId);
            
            return ResultadoT<AutenticacionRespuesta>.Fallo(Error.Fallo("402", "Tu cuenta aún no ha sido confirmada"));
        }

        var refrescado = tokenRefrescado.GenerarTokenRefrescado();
        
        respuesta.JwtToken = await token.GenerarTokenJwt(usuario, cancellationToken);
        respuesta.RefreshToken = refrescado.Token;
        
        return ResultadoT<AutenticacionRespuesta>.Exito(respuesta);
    }
    
}