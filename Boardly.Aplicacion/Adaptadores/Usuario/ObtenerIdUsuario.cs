using Boardly.Aplicacion.DTOs.Usuario;
using Boardly.Dominio.Puertos.CasosDeUso.Usuario;
using Boardly.Dominio.Puertos.Repositorios.Cuentas;
using Boardly.Dominio.Utilidades;
using Microsoft.Extensions.Logging;

namespace Boardly.Aplicacion.Adaptadores.Usuario;

public class ObtenerIdUsuario(
    IUsuarioRepositorio repositorioUsuario,
    ILogger<IUsuarioRepositorio> logger
    ) : IObtenerIdUsuario<UsuarioDto>
{
    public async Task<ResultadoT<UsuarioDto>> ObtenerIdUsarioAsync(Guid id, CancellationToken cancellationToken)
    {
        var usuarioEntidad = await repositorioUsuario.ObtenerByIdAsync(id, cancellationToken);
        
        if (usuarioEntidad is null)
        {
            logger.LogWarning("No se encontró ningún usuario con el ID {UsuarioId}.", id);
            return ResultadoT<UsuarioDto>.Fallo(
                Error.NoEncontrado("404", $"No se encontró un usuario con el ID {id}.")
            );
        }

        UsuarioDto usuarioDto = new(
            UsuarioId: usuarioEntidad.UsuarioId,
            Nombre: usuarioEntidad.Nombre,
            Apellido: usuarioEntidad.Apellido,
            Correo: usuarioEntidad.Correo,
            NombreUsuario: usuarioEntidad.NombreUsuario,
            FechaCreacion: usuarioEntidad.FechaCreacion,
            Estado: usuarioEntidad.Estado,
            FotoPerfil: usuarioEntidad.FotoPerfil,
            FechaRegistro: usuarioEntidad.FechaRegistro
        );

        logger.LogInformation("Usuario con ID {UsuarioId} obtenido correctamente.", id);

        return ResultadoT<UsuarioDto>.Exito(usuarioDto);
    }

}