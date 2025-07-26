using Boardly.Aplicacion.DTOs.Ceo;
using Boardly.Dominio.Puertos.CasosDeUso.Ceo;
using Boardly.Dominio.Puertos.Repositorios.Cuentas;
using Boardly.Dominio.Utilidades;
using Microsoft.Extensions.Logging;

namespace Boardly.Aplicacion.Adaptadores.Ceo;

public class ObtenerConteoDeEstadisticasCeo(
    ILogger<ObtenerConteoDeEstadisticasCeo> logger,
    ICeoRepositorio ceoRepositorio
    ) : IObtenerConteoDeEstadisticasCeo<CeoEstadisticaDto>
{
    public async Task<ResultadoT<CeoEstadisticaDto>> ObtenerConteoDeEstadisticasCeoAsync(Guid ceoId,
        CancellationToken cancellationToken)
    {
        var ceo = await ceoRepositorio.ObtenerByIdAsync(ceoId, cancellationToken);
        if (ceo is null)
        {
            logger.LogWarning("No se encontró ningún CEO con el ID: {CeoId}", ceoId);
            
            return ResultadoT<CeoEstadisticaDto>.Fallo(Error.Fallo("404", "Ceo no encontrado"));
        }

        var conteoTotalEmpresas = await ceoRepositorio.ObtenerConteoDeEmpresasCeoAsync(ceoId, cancellationToken);

        var conteoTotalProyectos = await ceoRepositorio.ObtenerConteoDeProyectosAsync(ceoId, cancellationToken);

        var conteoTotalEmpleados = await ceoRepositorio.ObtenerConteoDeEmpleadosDentroDeLaEmpresaAsync(ceoId, cancellationToken);
        
        logger.LogInformation("Ceo con ID {CeoId} encontrado correctamente.", ceo.CeoId);
        
        return ResultadoT<CeoEstadisticaDto>.Exito(new CeoEstadisticaDto(conteoTotalEmpresas, conteoTotalProyectos, conteoTotalEmpleados));
    }
}