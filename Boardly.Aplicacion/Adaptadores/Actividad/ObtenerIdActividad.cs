using Boardly.Aplicacion.DTOs.Actividad;
using Boardly.Dominio.Puertos.CasosDeUso.Actividad;
using Boardly.Dominio.Puertos.Repositorios;
using Boardly.Dominio.Utilidades;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace Boardly.Aplicacion.Adaptadores.Actividad;

public class ObtenerIdActividad(
    ILogger<ObtenerIdActividad> logger,
    IActividadRepositorio actividadRepositorio,
    IDistributedCache cache
    ) : IObtenerIdActividad<ActividadDto>
{
    public async Task<ResultadoT<ActividadDto>> ObtenerIdActividadAsync(Guid id, CancellationToken cancellationToken)
    {
        var actividad = await cache.ObtenerOCrearAsync(
            $"obtener-actividad-id-{id}",
            async () => await actividadRepositorio.ObtenerByIdAsync(id, cancellationToken),
            cancellationToken: cancellationToken
        );

        if (actividad is null)
        {
            logger.LogWarning("No se encontró la actividad con ID: {ActividadId}.", id);

            return ResultadoT<ActividadDto>.Fallo(
                Error.NoEncontrado("404", "La actividad especificada no fue encontrada."));
        }

        ActividadDto actividadDto = new
        (
            actividad.ActividadId,
            actividad.Nombre!,
            actividad.Prioridad!,
            actividad.Descripcion!,
            actividad.Estado!,
            actividad.FechaInicio,
            actividad.FechaFinalizacion,
            actividad.Orden
        );

        logger.LogInformation("Se obtuvo la actividad con ID: {ActividadId} con éxito.", actividad.ActividadId);

        return ResultadoT<ActividadDto>.Exito(actividadDto);
    }

}