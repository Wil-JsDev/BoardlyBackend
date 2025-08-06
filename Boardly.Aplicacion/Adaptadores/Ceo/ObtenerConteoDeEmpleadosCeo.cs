using Boardly.Aplicacion.DTOs.Ceo;
using Boardly.Aplicacion.Mapper;
using Boardly.Dominio.Puertos.CasosDeUso.Ceo;
using Boardly.Dominio.Puertos.Repositorios.Cuentas;
using Boardly.Dominio.Utilidades;
using Microsoft.Extensions.Logging;

namespace Boardly.Aplicacion.Adaptadores.Ceo;

public class ObtenerConteoDeEmpleadosCeo(
    ILogger<ObtenerConteoDeEmpleadosCeo> logger,
    ICeoRepositorio ceoRepositorio
    ) : IObtenerConteoDeEmpleadosCeo<ConteoEmpleadosCeoDto>
{
    public async Task<ResultadoT<ConteoEmpleadosCeoDto>> ObtenerConteoDeEmpleadosCeoAsync(Guid ceoId, CancellationToken cancellationToken)
    {
        var ceo = await ceoRepositorio.ObtenerByIdAsync(ceoId, cancellationToken);
        if (ceo is null)
        {
            logger.LogWarning("No se encontró el CEO con ID: {CeoId}", ceoId);
    
            return ResultadoT<ConteoEmpleadosCeoDto>.Fallo(Error.Fallo("404", "Ceo no encontrado"));
        }
        
        var resultado = await ceoRepositorio.MapearConteoEmpleadoCeoAsync(ceoId, logger, cancellationToken);
        
        return resultado;
    }
}