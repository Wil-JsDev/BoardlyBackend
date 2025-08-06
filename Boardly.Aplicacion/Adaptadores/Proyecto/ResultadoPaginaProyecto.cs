using Boardly.Aplicacion.DTOs.Paginacion;
using Boardly.Aplicacion.DTOs.Proyecto;
using Boardly.Dominio.Helper;
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
) : IResultadoPaginaProyecto<PaginacionParametro, ProyectoDetallesConConteoDto>
{
    public async Task<ResultadoT<ResultadoPaginado<ProyectoDetallesConConteoDto>>> ObtenerPaginacionProyectoAsync(Guid empresaId, PaginacionParametro solicitud, CancellationToken cancellationToken)
    {
        if (solicitud.TamanoPagina <= 0 || solicitud.NumeroPagina <= 0)
        {
            logger.LogWarning("Parámetros inválidos de paginación recibidos: TamañoPagina={TamanoPagina}, NumeroPagina={NumeroPagina}", 
                solicitud.TamanoPagina, solicitud.NumeroPagina);
        
            return ResultadoT<ResultadoPaginado<ProyectoDetallesConConteoDto>>.Fallo(
                Error.Fallo("400", "Los parámetros de paginación deben ser mayores a cero."));
        }
        
        var resultadoPaginacion = await repositorioProyecto.ObtenerPaginasProyectoAsync(
            empresaId,
            solicitud.NumeroPagina,
            solicitud.TamanoPagina,
            cancellationToken);

        string cacheKey = $"obtener-paginacion-proyecto-{empresaId}-{solicitud.NumeroPagina}-{solicitud.TamanoPagina}";
        var resultaPaginacionDto = await cache.ObtenerOCrearAsync(cacheKey,
        async () =>
            {
                var proyectosIds = resultadoPaginacion.Elementos!
                    .Select(x => x.ProyectoId)
                    .ToList();

                var conteoDeActividades = new List<int>();
                var conteoDeTareas = new List<int>();
                var conteoDeTareasCompletadas = new List<int>();
                var conteoDeTareasPendientes = new List<int>();

                foreach (var id in proyectosIds)
                {
                    var actividades = await repositorioProyecto.ObtenerConteoDeActividadesProyectosAsync(id, cancellationToken);
                    var tareas = await repositorioProyecto.ObtenerConteoDeTareasProyectosAsync(id, cancellationToken);
                    var tareasCompletadas = await repositorioProyecto.ObtenerConteoDeTareasCompletadasProyectosAsync(id, cancellationToken);
                    var tareasPendientes = await repositorioProyecto.ObtenerConteoDeTareasPendienteProyectosAsync(id, cancellationToken);

                    conteoDeActividades.Add(actividades);
                    conteoDeTareas.Add(tareas);
                    conteoDeTareasCompletadas.Add(tareasCompletadas);
                    conteoDeTareasPendientes.Add(tareasPendientes);
                }

                var proyectosDto = resultadoPaginacion.Elementos!
                    .Select((x, index) => new ProyectoDetallesConConteoDto(
                        x.ProyectoId,
                        x.EmpresaId,
                        x.Nombre,
                        x.Descripcion,
                        x.FechaInicio,
                        x.FechaFin,
                        x.Estado.ToString(),
                        x.FechaCreado,
                        ProyectoConteo: new ProyectoConteoDto
                        (
                            ConteoActividades: conteoDeActividades[index],
                            ConteoTareas: conteoDeTareas[index],
                            ConteoTareasCompletadas: conteoDeTareasCompletadas[index],
                            ConteoTareasPendientes: conteoDeTareasPendientes[index]
                        )
                    ))
                    .ToList();

                var totalElementos = proyectosDto.Count;

                var elementosPaginados = proyectosDto
                    .Paginar(solicitud.NumeroPagina, solicitud.TamanoPagina)
                    .ToList();

                return new ResultadoPaginado<ProyectoDetallesConConteoDto>(
                    elementos: elementosPaginados,
                    totalElementos: totalElementos,
                    paginaActual: solicitud.NumeroPagina,
                    tamanioPagina: solicitud.TamanoPagina
                );
            },
            cancellationToken: cancellationToken
        );

        
        logger.LogInformation("Se obtuvo correctamente la página {NumeroPagina} de proyectos para la empresa {EmpresaId}. Total en esta página: {CantidadProyectos}",
            solicitud.NumeroPagina, empresaId, resultaPaginacionDto.Elementos!.Count());

        return ResultadoT<ResultadoPaginado<ProyectoDetallesConConteoDto>>.Exito(resultaPaginacionDto);
    }
}