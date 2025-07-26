using Boardly.Aplicacion.DTOs.Tarea;
using Boardly.Dominio.Puertos.CasosDeUso.Tarea;
using Boardly.Dominio.Puertos.Repositorios;
using Boardly.Dominio.Utilidades;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace Boardly.Aplicacion.Adaptadores.Tarea;

public class ObtenerIdTarea(
    ILogger<ObtenerIdTarea> logger,
    ITareaRepositorio tareaRepositorio,
    IDistributedCache cache
    ) : IObtenerIdTarea<TareaDto>
{
    public async Task<ResultadoT<TareaDto>> ObtenerIdUsuarioAsync(Guid tareaId, CancellationToken cancellationToken)
    {
        var tarea = await cache.ObtenerOCrearAsync(
            $"obtener-tarea-{tareaId}",
            async () => await tareaRepositorio.ObtenerByIdAsync(tareaId, cancellationToken),
            cancellationToken: cancellationToken
        );

        if (tarea is null)
        {
            logger.LogWarning("No se encontró ninguna tarea con el ID: {TareaId}", tareaId);

            return ResultadoT<TareaDto>.Fallo(
                Error.NoEncontrado("404", "Este ID de tarea no existe."));
        }

        var tareaDto = new TareaDto
        (
            tarea.TareaId,
            tarea.ProyectoId,
            tarea.Titulo,
            tarea.Estado,
            tarea.Descripcion,
            tarea.FechaInicio,
            tarea.FechaVencimiento,
            tarea.FechaActualizacion,
            tarea.FechaCreado,
            tarea.ActividadId
        );

        logger.LogInformation("Tarea encontrada con éxito. ID: {TareaId}, Título: {Titulo}", tarea.TareaId, tarea.Titulo);

        return ResultadoT<TareaDto>.Exito(tareaDto);
    }

}