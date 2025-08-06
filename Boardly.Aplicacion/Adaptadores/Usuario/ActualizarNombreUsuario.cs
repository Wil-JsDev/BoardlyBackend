using Boardly.Aplicacion.DTOs.Usuario;
using Boardly.Dominio.Puertos.CasosDeUso.Usuario;
using Boardly.Dominio.Puertos.Repositorios.Cuentas;
using Boardly.Dominio.Utilidades;
using Microsoft.Extensions.Logging;

namespace Boardly.Aplicacion.Adaptadores.Usuario;

public class ActualizarNombreUsuario(
    IUsuarioRepositorio repositorioUsuario,
    ILogger<ActualizarNombreUsuario> logger
    ) : IActualizarNombreUsuario<ActualizarPropiedadUsuarioDto,ActualizarUsuarioDto>
{
    public async Task<ResultadoT<ActualizarUsuarioDto>> ActualizarNombreUsuarioAsync(Guid id, ActualizarPropiedadUsuarioDto solicitud, CancellationToken cancellationToken)
    {
        if (solicitud is null)
        {
            logger.LogWarning("La solicitud para actualizar el usuario con ID {UsuarioId} es nula.", id);
            return ResultadoT<ActualizarUsuarioDto>.Fallo(Error.Fallo("400", "La solicitud no puede ser nula."));
        }

        var usuario = await repositorioUsuario.ObtenerByIdAsync(id, cancellationToken);
        if (usuario is null)
        {
            logger.LogWarning("No se encontró ningún usuario con ID {UsuarioId}.", id);
            return ResultadoT<ActualizarUsuarioDto>.Fallo(Error.NoEncontrado("404", $"No se encontró un usuario con el ID {id}."));
        }
        
        usuario.Nombre = solicitud.propiedad;
        usuario.FechaActualizacion = DateTime.UtcNow;

        await repositorioUsuario.ActualizarAsync(usuario, cancellationToken);

        logger.LogInformation("Usuario con ID {UsuarioId} actualizado correctamente.",
            id);

        ActualizarUsuarioDto usuarioDto = new
        (
            Nombre: usuario.Nombre,
            Apellido: usuario.Apellido
        );

        return ResultadoT<ActualizarUsuarioDto>.Exito(usuarioDto);
    }

}