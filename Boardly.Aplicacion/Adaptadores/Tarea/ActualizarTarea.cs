using Boardly.Aplicacion.DTOs.Tarea;
using Boardly.Dominio.Puertos.CasosDeUso.Tarea;
using Boardly.Dominio.Puertos.Repositorios;
using Boardly.Dominio.Utilidades;
using Microsoft.Extensions.Logging;

namespace Boardly.Aplicacion.Adaptadores.Tarea;

public class ActualizarTarea(
    ILogger<ActualizarTarea> logger,
    ITareaRepositorio tareaRepositorio
    ) : IActualizarTarea<ActualizarTituloTareaDto, ActualizarTituloTareaDto>
{
    public async Task<ResultadoT<ActualizarTituloTareaDto>> ActualizarTareaAsync(Guid tareaId, ActualizarTituloTareaDto solicitud, CancellationToken cancellationToken)
    {
        var tarea = await tareaRepositorio.ObtenerByIdAsync(tareaId, cancellationToken);
        if (tarea is null)
        {
            logger.LogWarning("No se encontró la tarea con ID: {TareaId}", tareaId);

            return ResultadoT<ActualizarTituloTareaDto>.Fallo(
                Error.NoEncontrado("404", "Este ID de tarea no existe."));
        }
        
        tarea.Titulo = solicitud.Titulo;
        tarea.FechaActualizacion = DateTime.UtcNow;

        await tareaRepositorio.ActualizarAsync(tarea, cancellationToken);

        logger.LogInformation("Tarea con ID {TareaId} actualizada exitosamente. Nuevo título: '{NuevoTitulo}'", 
            tareaId, tarea.Titulo);

        var actualizarDto = new ActualizarTituloTareaDto(tarea.Titulo);

        return ResultadoT<ActualizarTituloTareaDto>.Exito(actualizarDto);
    }

}