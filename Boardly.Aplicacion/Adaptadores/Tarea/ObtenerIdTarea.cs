using Boardly.Aplicacion.DTOs.Tarea;
using Boardly.Aplicacion.Mapper;
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
    ) : IObtenerIdTarea<TareaDetalles>
{
    public async Task<ResultadoT<TareaDetalles>> ObtenerIdUsuarioAsync(Guid tareaId, CancellationToken cancellationToken)
    {
        var tarea = await cache.ObtenerOCrearAsync(
            $"obtener-tarea-{tareaId}",
            async () =>
            {
                var tareaDetalle = await tareaRepositorio.ObtenerDetallesPorTareaIdAsync(tareaId, cancellationToken);

                var tareaDetallesDto = TareaMapper.MapToDetalles(tareaDetalle!);
                
                return tareaDetallesDto;
            },
            cancellationToken: cancellationToken
        );

        if (tarea is null)
        {
            logger.LogWarning("No se encontró ninguna tarea con el ID: {TareaId}", tareaId);

            return ResultadoT<TareaDetalles>.Fallo(
                Error.NoEncontrado("404", "Este ID de tarea no existe."));
        }
        
        logger.LogInformation("Tarea encontrada con éxito. Título: {Titulo}", tarea.Titulo);

        return ResultadoT<TareaDetalles>.Exito(tarea);
    }

}