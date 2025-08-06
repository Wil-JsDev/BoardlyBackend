using Boardly.Dominio.Puertos.CasosDeUso.Proyecto;
using Boardly.Dominio.Puertos.Repositorios;
using Boardly.Dominio.Utilidades;
using Microsoft.Extensions.Logging;

namespace Boardly.Aplicacion.Adaptadores.Proyecto;

public class BorrarProyecto(
    ILogger<BorrarProyecto> logger,
    IProyectoRepositorio proyectoRepositorio
    ) : IBorrarProyecto
{
    public async Task<ResultadoT<Guid>> BorrarProyectoAsync(Guid id, CancellationToken cancellationToken)
    {
        var proyecto = await proyectoRepositorio.ObtenerByIdAsync(id, cancellationToken);
        if (proyecto is null)
        {
            logger.LogWarning("No se encontró el proyecto con ID: {ProyectoId} para eliminar.", id);
        
            return ResultadoT<Guid>.Fallo(Error.NoEncontrado("404", "El proyecto especificado no fue encontrado."));
        }
    
        await proyectoRepositorio.EliminarAsync(proyecto, cancellationToken);
    
        logger.LogInformation("Proyecto eliminado exitosamente. ID: {ProyectoId}", proyecto.ProyectoId);
    
        return ResultadoT<Guid>.Exito(proyecto.ProyectoId);
    }

}