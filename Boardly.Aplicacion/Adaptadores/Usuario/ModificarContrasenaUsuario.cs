using Boardly.Aplicacion.DTOs.Codigo;
using Boardly.Aplicacion.DTOs.Contrasena;
using Boardly.Dominio.Puertos.CasosDeUso.Codigo;
using Boardly.Dominio.Puertos.CasosDeUso.Usuario;
using Boardly.Dominio.Puertos.Repositorios.Cuentas;
using Boardly.Dominio.Utilidades;
using Microsoft.Extensions.Logging;

namespace Boardly.Aplicacion.Adaptadores.Usuario;

public class ModificarContrasenaUsuario(
    ILogger<ModificarContrasenaUsuario> logger,
    IUsuarioRepositorio repositorioUsuario,
    IBuscarCodigo<CodigoDto> codigo
) : IModificarContrasenaUsuario<ModificarContrasenaUsuarioDto>
{
    public async Task<ResultadoT<string>> ModificarContasenaAsync(
        ModificarContrasenaUsuarioDto solicitud, 
        CancellationToken cancellationToken
    )
    {
        var usuarioEntidad = await repositorioUsuario.ObtenerByIdAsync(solicitud.UsuarioId, cancellationToken);
        
        if (usuarioEntidad is null)
        {
            logger.LogWarning("No se encontró ningún usuario con el ID {UsuarioId}.", solicitud);
            return ResultadoT<string>.Fallo(
                Error.NoEncontrado("404", $"No se encontró un usuario con el ID {solicitud}.")
            );
        }
        
        var codigoEntidad = await codigo.BuscarCodigoAsync(solicitud.Codigo, cancellationToken);
        if (codigoEntidad is null)
        {
            logger.LogWarning("");
            
            return ResultadoT<string>.Fallo(Error.NoEncontrado("404", "Código no encontrado"));
        }

        if (codigoEntidad.Valor.UsuarioId != solicitud.UsuarioId)
        {
            logger.LogWarning("");
            
            return ResultadoT<string>.Fallo(Error.NoEncontrado("403", "El código no corresponde a este usuario"));
        }
        
        var contrasenaNueva = BCrypt.Net.BCrypt.HashPassword(solicitud.ConfirmacionDeContrsena);
        
        await repositorioUsuario.ActualizarContrasenaAsync(usuarioEntidad, contrasenaNueva, cancellationToken);
        
        logger.LogInformation("");
        
        return ResultadoT<string>.Exito("La contraseña ha sido actualizada correctamente.");
    }
}