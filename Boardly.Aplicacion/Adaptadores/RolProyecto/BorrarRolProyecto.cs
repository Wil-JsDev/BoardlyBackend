using Boardly.Dominio.Puertos.CasosDeUso.RolProyecto;
using Boardly.Dominio.Puertos.Repositorios;
using Boardly.Dominio.Utilidades;
using Microsoft.Extensions.Logging;

namespace Boardly.Aplicacion.Adaptadores.RolProyecto;

public class BorrarRolProyecto(
    ILogger<BorrarRolProyecto> logger,
    IRolProyectoRepositorio rolProyectoRepositorio
    ) : IBorrarRolProyecto
{
    public async Task<ResultadoT<Guid>> BorrarRolProyectoAsync(Guid id, CancellationToken cancellationToken)
    {
        var rolProyecto = await rolProyectoRepositorio.ObtenerByIdAsync(id, cancellationToken);
        if (rolProyecto is null)
        {
            logger.LogWarning("Este id del rol de proyecto no existe. Id: {RolProyectoId}", id);
            
            return ResultadoT<Guid>.Fallo(Error.Fallo("400", "Este id del rol de proyecto no existe."));
        }
        
        await rolProyectoRepositorio.EliminarAsync(rolProyecto, cancellationToken);
        
        logger.LogInformation("Rol de proyecto eliminado exitosamente. ID: {RolProyectoId}", rolProyecto.RolProyectoId);
        
        return ResultadoT<Guid>.Exito(rolProyecto.RolProyectoId);
    }
}