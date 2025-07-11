using Boardly.Aplicacion.DTOs.Paginacion;
using Boardly.Aplicacion.DTOs.RolProyecto;
using Boardly.Dominio.Puertos.CasosDeUso.RolProyecto;
using Boardly.Dominio.Puertos.Repositorios;
using Boardly.Dominio.Utilidades;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace Boardly.Aplicacion.Adaptadores.RolProyecto;

public class ResultadoPaginadoRolProyecto(
    ILogger<ResultadoPaginadoRolProyecto> logger,
    IRolProyectoRepositorio rolProyectoRepositorio,
    IProyectoRepositorio proyectoRepositorio,
    IDistributedCache cache
    ) : IResultadoPaginadoRolProyecto<PaginacionParametro, RolProyectoDto>
{
    public async Task<ResultadoT<ResultadoPaginado<RolProyectoDto>>> ObtenerPaginacionRolProyectoAsync(Guid proyectoId, PaginacionParametro solicitud,
        CancellationToken cancellationToken)
    {
        var proyecto = await proyectoRepositorio.ObtenerByIdAsync(proyectoId, cancellationToken);
        if (proyecto is null)
        {
            logger.LogWarning("Este {ProyectoId} id no existe", proyectoId);
            
            return ResultadoT<ResultadoPaginado<RolProyectoDto>>.Fallo(Error.Fallo("400", "Este id del proyecto no existe."));
        }

        var resultadoPaginado = await cache.ObtenerOCrearAsync(
            $"obtener-paginacion-rol-proyecto-{solicitud.NumeroPagina}-{solicitud.TamanoPagina}",
            async () => await rolProyectoRepositorio.ObtenerPaginasRolProyectoAsync(proyectoId, solicitud.NumeroPagina,
                solicitud.TamanoPagina, cancellationToken),
            cancellationToken: cancellationToken
        );

        var rolProyectoDto = resultadoPaginado.Elementos!.Select(x => new RolProyectoDto
        (
            RolProyectoId: x.RolProyectoId,
            Nombre: x.Nombre,
            Descripcion: x.Descripcion!
        )).ToList();

        if (!rolProyectoDto.Any())
        {
            logger.LogWarning("La lista esta vacia");
            
            return ResultadoT<ResultadoPaginado<RolProyectoDto>>.Fallo(Error.Fallo("404", "No se encontraron roles de proyecto para los parametros de paginacion especificados."));
        }
        
        var resultadoPaginadoDto = new ResultadoPaginado<RolProyectoDto>(
            elementos: rolProyectoDto.ToList(),
            totalElementos: resultadoPaginado.TotalElementos,
            paginaActual: solicitud.NumeroPagina,
            tamanioPagina: solicitud.TamanoPagina
        );
        
        
        logger.LogInformation("Se obtuvo la pagina {NumeroPagina} de roles de proyecto con exito. Cantidad de roles de proyecto en esta pagina: {CantidadRolesDeProyecto}",
            solicitud.NumeroPagina, resultadoPaginadoDto.Elementos!.Count());
        
        return ResultadoT<ResultadoPaginado<RolProyectoDto>>.Exito(resultadoPaginadoDto);
    }
}