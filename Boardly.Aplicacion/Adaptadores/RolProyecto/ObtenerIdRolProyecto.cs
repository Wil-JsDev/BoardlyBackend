using Boardly.Aplicacion.DTOs.RolProyecto;
using Boardly.Dominio.Puertos.CasosDeUso.RolProyecto;
using Boardly.Dominio.Puertos.Repositorios;
using Boardly.Dominio.Utilidades;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace Boardly.Aplicacion.Adaptadores.RolProyecto;

public class ObtenerIdRolProyecto(
    IRolProyectoRepositorio rolProyectoRepositorio,
    ILogger<ObtenerIdRolProyecto> logger  ,
    IDistributedCache cache
    ) : IObtenerIdRolProyecto<RolProyectoDto>
{
    public async Task<ResultadoT<RolProyectoDto>> ObtenerIdRolProyectoAsync(Guid id, CancellationToken cancellationToken)
    {
        var rolProyecto = await cache.ObtenerOCrearAsync(
            $"obtener-rol-proyecto-{id}",
            async () => await rolProyectoRepositorio.ObtenerByIdAsync(id, cancellationToken),
            cancellationToken: cancellationToken
        );
    
        if (rolProyecto is null)
        {
            logger.LogWarning("No se encontró un rol de proyecto con el ID: {RolProyectoId}", id);
            
            return ResultadoT<RolProyectoDto>.Fallo(Error.Fallo("400", "Este id del rol de proyecto no existe."));
        }

        var rolProyectoDto = new RolProyectoDto(
            RolProyectoId: rolProyecto.RolProyectoId,
            Nombre: rolProyecto.Nombre,
            Descripcion: rolProyecto.Descripcion ?? string.Empty
        );

        logger.LogInformation("Se obtuvo correctamente el rol de proyecto con ID: {RolProyectoId}", rolProyecto.RolProyectoId);
    
        return ResultadoT<RolProyectoDto>.Exito(rolProyectoDto);
    }
}