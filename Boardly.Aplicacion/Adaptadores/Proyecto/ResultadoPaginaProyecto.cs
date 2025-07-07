using Boardly.Aplicacion.DTOs.Paginacion;
using Boardly.Aplicacion.DTOs.Proyecto;
using Boardly.Dominio.Puertos.CasosDeUso.Proyecto;
using Boardly.Dominio.Puertos.Repositorios;
using Boardly.Dominio.Utilidades;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace Boardly.Aplicacion.Adaptadores.Proyecto;

public class ResultadoPaginaProyecto(
    ILogger<ResultadoPaginaProyecto> logger,
    IProyectoRepositorio repositorioProyecto,
    IDistributedCache cache
    ) : IResultadoPaginaProyecto<PaginacionParametro, ProyectoDto>
{
    public async Task<ResultadoT<ResultadoPaginado<ProyectoDto>>> ObtenerPaginacionProyectoAsync(
        Guid empresaId,
        PaginacionParametro solicitud,
        CancellationToken cancellationToken)
    {
        if (solicitud.TamanoPagina <= 0 || solicitud.NumeroPagina <= 0)
        {
            logger.LogWarning("Parámetros inválidos de paginación recibidos: TamañoPagina={TamanoPagina}, NumeroPagina={NumeroPagina}", 
                solicitud.TamanoPagina, solicitud.NumeroPagina);
        
            return ResultadoT<ResultadoPaginado<ProyectoDto>>.Fallo(
                Error.Fallo("400", "Los parámetros de paginación deben ser mayores a cero."));
        }

        string cacheKey = $"obtener-paginacion-proyecto-{empresaId}-{solicitud.NumeroPagina}-{solicitud.TamanoPagina}";

        var resultadoPaginacion = await cache.ObtenerOCrearAsync(
            cacheKey,
            async () => await repositorioProyecto.ObtenerPaginasProyectoAsync(
                empresaId,
                solicitud.NumeroPagina,
                solicitud.TamanoPagina,
                cancellationToken),
            cancellationToken: cancellationToken
        );

        var proyectosDto = resultadoPaginacion.Elementos!
            .Select(x => new ProyectoDto(
                x.ProyectoId,
                x.EmpresaId,
                x.Nombre,
                x.Descripcion,
                x.FechaInicio,
                x.FechaFin,
                x.Estado.ToString(),
                x.FechaCreado
            ))
            .ToList();

        var resultadoPaginado = new ResultadoPaginado<ProyectoDto>(
            elementos: proyectosDto,
            totalElementos: resultadoPaginacion.TotalElementos,
            paginaActual: solicitud.NumeroPagina,
            tamanioPagina: solicitud.TamanoPagina
        );

        logger.LogInformation("Se obtuvo correctamente la página {NumeroPagina} de proyectos para la empresa {EmpresaId}. Total en esta página: {CantidadProyectos}",
            solicitud.NumeroPagina, empresaId, proyectosDto.Count);

        return ResultadoT<ResultadoPaginado<ProyectoDto>>.Exito(resultadoPaginado);
    }

}