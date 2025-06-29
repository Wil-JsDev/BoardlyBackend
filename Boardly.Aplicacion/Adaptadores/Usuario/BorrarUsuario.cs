using Boardly.Dominio.Puertos.CasosDeUso.Usuario;
using Boardly.Dominio.Puertos.Repositorios.Cuentas;
using Boardly.Dominio.Utilidades;
using Microsoft.Extensions.Logging;

namespace Boardly.Aplicacion.Adaptadores.Usuario;

public class BorrarUsuario(
    IUsuarioRepositorio repositorioUsuario,
    ILogger<BorrarUsuario>  logger
    ) : IBorrarUsuario
{
    public async Task<ResultadoT<Guid>> BorrarUsuarioAsync(Guid id, CancellationToken cancellationToken)
    {
        var usuario = await repositorioUsuario.ObtenerByIdAsync(id, cancellationToken);
        if (usuario is null)
        {
            logger.LogWarning("Intento de eliminación fallido. No se encontró ningún usuario con ID {UsuarioId}.", id);
            return ResultadoT<Guid>.Fallo(
                Error.NoEncontrado("404", $"No se encontró un usuario con el ID {id}.")
            );
        }

        await repositorioUsuario.EliminarAsync(usuario, cancellationToken);

        logger.LogInformation("Usuario con ID {UsuarioId} eliminado correctamente.", usuario.UsuarioId);

        return ResultadoT<Guid>.Exito(usuario.UsuarioId);
    }
}