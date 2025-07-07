using Boardly.Dominio.Puertos.CasosDeUso.Actividad;
using Boardly.Dominio.Puertos.Repositorios;
using Boardly.Dominio.Utilidades;
using Microsoft.Extensions.Logging;

namespace Boardly.Aplicacion.Adaptadores.Actividad;

public class BorrarActividad(
    ILogger<BorrarActividad> logger,
    IActividadRepositorio actividadRepositorio 
    ) : IBorrarActividad
{
    public async Task<ResultadoT<Guid>> BorrarActividadAsync(Guid id, CancellationToken cancellationToken)
    {
        var actividad = await actividadRepositorio.ObtenerByIdAsync(id, cancellationToken);

        if (actividad is null)
        {
            logger.LogWarning("No se encontró la actividad con ID: {ActividadId} al intentar eliminarla.", id);

            return ResultadoT<Guid>.Fallo(
                Error.NoEncontrado("404", "La actividad especificada no fue encontrada."));
        }
    
        await actividadRepositorio.EliminarAsync(actividad, cancellationToken);

        logger.LogInformation("Actividad eliminada exitosamente. ID: {ActividadId}", actividad.ActividadId);

        return ResultadoT<Guid>.Exito(actividad.ActividadId);
    }

}