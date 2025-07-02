using Boardly.Aplicacion.DTOs.Autenticacion;
using Boardly.Dominio.Puertos.CasosDeUso.Autenticacion;
using Boardly.Dominio.Puertos.Repositorios.Cuentas;
using Boardly.Dominio.Utilidades;
using Microsoft.Extensions.Logging;

namespace Boardly.Aplicacion.Adaptadores.Autenticacion;

public class Autenticacion(
    Logger<Autenticacion> logger,
    IUsuarioRepositorio usuarioRepositorio,
    IGenerarToken<Dominio.Modelos.Usuario> token): IAutenticacion<AutenticacionRespuesta, AutenticacionSolicitud>
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
            return ResultadoT<AutenticacionRespuesta>.Fallo(Error.Fallo("401", "El correo o la contraseña son incorrectos"));

        }

        if (!usuario.CuentaConfirmada)
        {
            return ResultadoT<AutenticacionRespuesta>.Fallo(Error.Fallo("402", "Tu cuenta aún no ha sido confirmada"));

        }
        
        //token 
        var tokenGenerado = token.GenerarTokenJwt(usuario);

        respuesta.Id = usuario.UsuarioId;
        respuesta.NombreUsuario = usuario.NombreUsuario;
        respuesta.Nombre = usuario.Nombre;
        respuesta.Apellido = usuario.Apellido;
        respuesta.Correo = usuario.Correo;
        
        //roles
        
        return ResultadoT<AutenticacionRespuesta>.Exito(respuesta);
    }
    
}