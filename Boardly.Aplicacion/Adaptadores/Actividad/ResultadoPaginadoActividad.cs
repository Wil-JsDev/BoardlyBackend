using Boardly.Aplicacion.DTOs.Actividad;
using Boardly.Aplicacion.DTOs.Paginacion;
using Boardly.Dominio.Helper;
using Boardly.Dominio.Puertos.CasosDeUso.Actividad;
using Boardly.Dominio.Puertos.Repositorios;
using Boardly.Dominio.Utilidades;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace Boardly.Aplicacion.Adaptadores.Actividad;

public class ResultadoPaginadoActividad(
    ILogger<ResultadoPaginadoActividad> logger,
    IActividadRepositorio actividadRepositorio,
    IDistributedCache cache
    ) : IResultadoPaginadoActividad<PaginacionParametro, ActividadDetallesDto>
{
    public async Task<ResultadoT<ResultadoPaginado<ActividadDetallesDto>>> ObtenerPaginacionActividadAsync(Guid proyectoId, PaginacionParametro solicitud, CancellationToken cancellationToken)
    {
        if (solicitud.NumeroPagina <= 0 || solicitud.TamanoPagina <= 0)
        {
            logger.LogWarning("Parámetros de paginación inválidos: NumeroPagina={NumeroPagina}, TamanoPagina={TamanoPagina}", 
                solicitud.NumeroPagina, solicitud.TamanoPagina);

            return ResultadoT<ResultadoPaginado<ActividadDetallesDto>>.Fallo(
                Error.Fallo("400", "Los parámetros de paginación deben ser mayores a cero."));
        }

        string cacheKey = $"obtener-paginado-actividad-{proyectoId}-{solicitud.NumeroPagina}-{solicitud.TamanoPagina}";

        var resultadoPagina = await actividadRepositorio.ObtenerPaginasActividadByIdProyectoAsync(
            proyectoId,
            solicitud.NumeroPagina,
            solicitud.TamanoPagina,
            cancellationToken);
        
        var resultadoPaginaDto = await cache.ObtenerOCrearAsync(cacheKey,
            async () =>
            {
                
                var actividadesIds = resultadoPagina.Elementos!.Select(x => x.ActividadId).ToList();
                
                var totalConteoDeTarea = actividadesIds
                    .Select(id => actividadRepositorio.ObtenerConteoDeTareasActividadAsync(id, cancellationToken))
                    .ToList();
                
                var totalConteoDeTareaCompletadas = actividadesIds
                    .Select(id => actividadRepositorio.ObtenerConteoDeTareasEnProcesoActividadAsync(id, cancellationToken))
                    .ToList();
                
                var totalConteoDeTareasFinalizadas = actividadesIds
                    .Select(id => actividadRepositorio.ObtenerConteoDeTareasFinalizadasActividadAsync(id, cancellationToken))
                    .ToList();
                
                await Task.WhenAll(totalConteoDeTarea);
                await Task.WhenAll(totalConteoDeTareaCompletadas);
                await Task.WhenAll(totalConteoDeTareasFinalizadas);
                
                var actividadesDto = resultadoPagina.Elementos!
                    .Select((x, index) => new ActividadDetallesDto(
                        ActividadId: x.ActividadId,
                        ProyectoId: x.ProyectoId,       
                        Nombre: x.Nombre!,
                        Prioridad: x.Prioridad!,
                        Descripcion: x.Descripcion!,
                        Estado: x.Estado!,
                        FechaInicio: x.FechaInicio,
                        FechaFin: x.FechaFinalizacion,
                        Orden: x.Orden,
                        ActividadConteo: new ActividadConteoDto
                        (
                            TotalTareas: totalConteoDeTarea[index].Result,
                            TotalTareasProceso: totalConteoDeTareaCompletadas[index].Result,
                            TotalTareasFinalizadas: totalConteoDeTareasFinalizadas[index].Result
                        )
                    ))
                    .ToList();
                
                var totalElementos = actividadesDto.Count();
                
                var elementosPaginados = actividadesDto
                    .Paginar(solicitud.NumeroPagina, solicitud.TamanoPagina)
                    .ToList();
                
                return  new ResultadoPaginado<ActividadDetallesDto>(
                    elementos: elementosPaginados,
                    totalElementos: totalElementos,
                    paginaActual: solicitud.NumeroPagina,
                    tamanioPagina: solicitud.TamanoPagina
                );
            }, 
                cancellationToken: cancellationToken
            );

        logger.LogInformation(
            "Se obtuvo exitosamente la página {NumeroPagina} de actividades. Total de actividades en esta página: {CantidadActividades}",
            solicitud.NumeroPagina, resultadoPaginaDto.Elementos!.Count());

        return ResultadoT<ResultadoPaginado<ActividadDetallesDto>>.Exito(resultadoPaginaDto);
    }

}