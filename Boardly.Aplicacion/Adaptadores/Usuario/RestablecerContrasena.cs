using Boardly.Aplicacion.DTOs.Contrasena;
using Boardly.Dominio.Puertos.CasosDeUso.Usuario;
using Boardly.Dominio.Puertos.Repositorios.Cuentas;
using Boardly.Dominio.Utilidades;
using Microsoft.Extensions.Logging;

namespace Boardly.Aplicacion.Adaptadores.Usuario;

public class RestablecerContrasena(
    ILogger<RestablecerContrasena> logger,
    IUsuarioRepositorio repositorioUsuario
    ) : IRestablecerContrasena<RestablecerContrasenaDto>
{
    public async Task<ResultadoT<string>> RestablecerContrasenaAsync(RestablecerContrasenaDto solicitud, CancellationToken cancellationToken)
    {
        var usuario = await repositorioUsuario.ObtenerByIdAsync(solicitud.UsuarioId, cancellationToken);
        if (usuario is null)
        {
            logger.LogInformation("");
            
            return ResultadoT<string>.Fallo(Error.NoEncontrado("404", "Usuario no encontrado"));
        }
        
        if (!BCrypt.Net.BCrypt.Verify(solicitud.ContrasenaAntigua, usuario.Contrasena))
        {
            logger.LogWarning("Actualización de contraseña fallida: la contraseña antigua no coincide para el usuario con ID '{UsuarioId}'.", usuario.UsuarioId);
            
            return ResultadoT<string>.Fallo(Error.Conflicto("409", "La contraseña antigua es incorrecta."));
        }
        
        var nuevaContrasena = BCrypt.Net.BCrypt.HashPassword(solicitud.ConfirmacionDeContrsena);
        
        await repositorioUsuario.ActualizarContrasenaAsync(usuario, nuevaContrasena, cancellationToken);
    
        logger.LogInformation("El usuario {UsuarioId} se la ha restablecido correctamente su contrasena.", usuario.UsuarioId);

        return ResultadoT<string>.Exito("La contrasena se ha actualizado correctamente.");
    }
}