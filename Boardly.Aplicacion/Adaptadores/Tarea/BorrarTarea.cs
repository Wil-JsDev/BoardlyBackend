using Boardly.Dominio.Puertos.CasosDeUso.Tarea;
using Boardly.Dominio.Puertos.Repositorios;
using Boardly.Dominio.Utilidades;
using Microsoft.Extensions.Logging;

namespace Boardly.Aplicacion.Adaptadores.Tarea;

public class BorrarTarea(
    ILogger<BorrarTarea> logger,
    ITareaRepositorio tareaRepositorio
    ) : IBorrarTarea
{
    public async Task<ResultadoT<Guid>> BorrarTareaAsync(Guid id, CancellationToken cancellationToken)
    {
        var tarea = await tareaRepositorio.ObtenerByIdAsync(id, cancellationToken);
        if (tarea is null)
        {
            logger.LogWarning("No se encontró ninguna tarea con el ID: {TareaId}", tarea.TareaId);

            return ResultadoT<Guid>.Fallo(
                Error.NoEncontrado("404", "Este ID de tarea no existe."));
        }
        
        await tareaRepositorio.EliminarAsync(tarea, cancellationToken);
        
        logger.LogInformation("Tarea con ID {TareaId} eliminada exitosamente.", tarea.TareaId);
        
        return ResultadoT<Guid>.Exito(tarea.TareaId);
    }
}