using Boardly.Dominio.Puertos.CasosDeUso.Ceo;
using Boardly.Dominio.Puertos.Repositorios.Cuentas;
using Boardly.Dominio.Utilidades;
using Microsoft.Extensions.Logging;

namespace Boardly.Aplicacion.Adaptadores.Ceo;

public class BorrarCeo(
    ILogger<BorrarCeo> logger,
    ICeoRepositorio ceoRepositorio
    ): IBorrarCeo
{
    public async Task<ResultadoT<Guid>> BorrarCeoAsync(Guid id, CancellationToken cancellationToken)
    {
        var ceo = await ceoRepositorio.ObtenerByIdAsync(id, cancellationToken);
        if (ceo is null)
        {
            logger.LogWarning("No se encontró ningun Ceo con el ID {CeoId} para eliminar.", id);
            Error.NoEncontrado("404", $"No se encontró ningun Ceo con el ID {id}.");
        }
        
        await ceoRepositorio.EliminarAsync(ceo, cancellationToken);
        logger.LogInformation("Ceo con ID {CeoId} eliminado correctamente.", ceo.CeoId);

        return ResultadoT<Guid>.Exito(ceo.CeoId);
    }
}