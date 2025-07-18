using Boardly.Aplicacion.DTOs.Paginacion;
using Boardly.Aplicacion.DTOs.Tarea;
using Boardly.Dominio.Puertos.CasosDeUso.Tarea;
using Boardly.Dominio.Puertos.Repositorios;
using Boardly.Dominio.Utilidades;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace Boardly.Aplicacion.Adaptadores.Tarea;

public class ResultadoPaginadoTarea(
    ILogger<ResultadoPaginadoTarea> logger,
    ITareaRepositorio repositorioTarea,
    IActividadRepositorio actividadRepositorio,
    IDistributedCache cache
    ) : IResultadoPaginadoTarea<PaginacionParametro, TareaDto>
{
       public async Task<ResultadoT<ResultadoPaginado<TareaDto>>> ObtenerPaginacionTareaAsync(
           Guid actividadId, 
           PaginacionParametro solicitud, 
           CancellationToken cancellationToken)
       {
            if (solicitud.TamanoPagina <= 0 || solicitud.NumeroPagina <= 0)
            {
                logger.LogWarning("Parámetros de paginación inválidos: TamañoPagina={TamanoPagina}, NumeroPagina={NumeroPagina}",
                    solicitud.TamanoPagina, solicitud.NumeroPagina);

                return ResultadoT<ResultadoPaginado<TareaDto>>.Fallo(
                    Error.Fallo("400", "Los parámetros de paginación deben ser mayores a 0."));
            }

            var actividad = await actividadRepositorio.ObtenerByIdAsync(actividadId, cancellationToken);
            if (actividad is null)
            {
                logger.LogWarning("Actividad no encontrada con ID: {ActividadId}", actividadId);

                return ResultadoT<ResultadoPaginado<TareaDto>>.Fallo(
                    Error.NoEncontrado("404", "La actividad especificada no fue encontrada."));
            }

            string cacheKey = $"obtener-paginado-tarea-{actividadId}-{solicitud.NumeroPagina}-{solicitud.TamanoPagina}";

            var resultadoPagina = await cache.ObtenerOCrearAsync(
                cacheKey,
                async () => await repositorioTarea.ObtenerPaginadoTareaAsync(
                    actividadId,
                    solicitud.NumeroPagina,
                    solicitud.TamanoPagina,
                    cancellationToken),
                cancellationToken: cancellationToken
            );

            if (resultadoPagina.Elementos == null)
            {
                logger.LogError("No se pudieron obtener las tareas paginadas para la actividad {ActividadId}", actividadId);

                return ResultadoT<ResultadoPaginado<TareaDto>>.Fallo(
                    Error.Fallo("500", "No se pudo obtener las tareas paginadas."));
            }

            var resultadoPaginaDto = resultadoPagina.Elementos.Select(x => new TareaDto
            (
                TareaId: x.TareaId,
                ProyectoId: x.ProyectoId,
                Titulo: x.Titulo,
                EstadoTarea: x.Estado,
                Descripcion: x.Descripcion,
                FechaInicio: x.FechaInicio,
                FechaVencimiento: x.FechaVencimiento,
                FechaActualizacion: x.FechaActualizacion,
                FechaCreado: x.FechaCreado,
                ActividadId: x.ActividadId
            )).ToList();

            var resultadoPaginado = new ResultadoPaginado<TareaDto>(
                elementos: resultadoPaginaDto,
                totalElementos: resultadoPagina.TotalElementos,
                paginaActual: solicitud.NumeroPagina,
                tamanioPagina: solicitud.TamanoPagina
            );

            logger.LogInformation("Paginación de tareas para actividad {ActividadId}: Página {Pagina}, Tamaño {Tamano}. Tareas obtenidas: {Cantidad}",
                actividadId, solicitud.NumeroPagina, solicitud.TamanoPagina, resultadoPaginaDto.Count);

            return ResultadoT<ResultadoPaginado<TareaDto>>.Exito(resultadoPaginado);
       }
}