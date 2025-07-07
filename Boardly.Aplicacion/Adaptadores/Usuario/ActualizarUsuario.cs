using Boardly.Aplicacion.DTOs.Usuario;
using Boardly.Dominio.Puertos.CasosDeUso.Usuario;
using Boardly.Dominio.Puertos.Repositorios.Cuentas;
using Boardly.Dominio.Utilidades;
using Microsoft.Extensions.Logging;

namespace Boardly.Aplicacion.Adaptadores.Usuario;

public class ActualizarUsuario(
    IUsuarioRepositorio repositorioUsuario,
    ILogger<ActualizarUsuario> logger
    ) : IActualizarUsuario<ActualizarUsuarioDto, ActualizarUsuarioDto>
{
    public async Task<ResultadoT<ActualizarUsuarioDto>> ActualizarUsuarioAsync(Guid id, ActualizarUsuarioDto solicitud, CancellationToken cancellationToken)
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

        if (await repositorioUsuario.NombreUsuarioEnUso(solicitud.NombreUsuario, usuario.UsuarioId, cancellationToken))
        {
            logger.LogWarning("El nombre de usuario '{NombreUsuario}' ya está en uso por otro usuario distinto al ID {UsuarioId}.",
                solicitud.NombreUsuario, usuario.UsuarioId);

            return ResultadoT<ActualizarUsuarioDto>.Fallo(
                Error.Conflicto("409", "El nombre de usuario ya está siendo utilizado por otro usuario.")
            );
        }

        if (await repositorioUsuario.EmailEnUsoAsync(solicitud.Correo, usuario.UsuarioId, cancellationToken))
        {
            logger.LogWarning("El correo electrónico '{Correo}' ya está en uso por otro usuario distinto al ID {UsuarioId}.",
                solicitud.Correo, usuario.UsuarioId);

            return ResultadoT<ActualizarUsuarioDto>.Fallo(
                Error.Conflicto("409", "El correo electrónico ya está siendo utilizado por otro usuario.")
            );
        }
        
        usuario.NombreUsuario = solicitud.NombreUsuario;
        usuario.Correo = solicitud.Correo;
        usuario.FechaActualizacion = DateTime.UtcNow;

        await repositorioUsuario.ActualizarAsync(usuario, cancellationToken);

        logger.LogInformation("Usuario con ID {UsuarioId} actualizado correctamente. Nuevo nombre de usuario: {NombreUsuario}, Nuevo correo: {Correo}",
            id, usuario.NombreUsuario, usuario.Correo);

        ActualizarUsuarioDto usuarioDto = new
        (
            NombreUsuario: usuario.NombreUsuario,
            Correo: usuario.Correo
        );

        return ResultadoT<ActualizarUsuarioDto>.Exito(usuarioDto);
    }

}