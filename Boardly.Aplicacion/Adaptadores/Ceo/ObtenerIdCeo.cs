using Boardly.Dominio.Puertos.CasosDeUso.Ceo;
using Boardly.Dominio.Puertos.Repositorios.Cuentas;
using Boardly.Dominio.Utilidades;
using Microsoft.Extensions.Logging;

namespace Boardly.Aplicacion.Adaptadores.Ceo;

public class ObtenerIdCeo(
    ILogger<ObtenerIdCeo> logger,
    ICeoRepositorio ceoRepositorio,
    IUsuarioRepositorio usuarioRepositorio
    ): IObtenerIdCeo
{
    public async Task<ResultadoT<Guid>> ObtenerIdCeoAsync(Guid id, CancellationToken cancellationToken)
    {
        var usuario = await usuarioRepositorio.ObtenerByIdAsync(id, cancellationToken);
        if (usuario is null)
        {
            logger.LogWarning("No se encontró el usuario con ID {UsuarioId}", id);
            return ResultadoT<Guid>.Fallo(Error.Fallo("404", "Usuario no encontrado"));
        }

        var ceoId = await ceoRepositorio.ObtenerIdCeoAsync(id, cancellationToken);
        if (ceoId == Guid.Empty)
        {
            logger.LogWarning("El usuario con ID {UsuarioId} no tiene rol de CEO", id);
            return ResultadoT<Guid>.Fallo(Error.Fallo("403", "El usuario no tiene rol de CEO"));
        }

        return ResultadoT<Guid>.Exito(ceoId);
    }

}