using Boardly.Aplicacion.DTOs.Empresa;
using Boardly.Aplicacion.DTOs.Paginacion;
using Boardly.Aplicacion.Mapper;
using Boardly.Dominio.Helper;
using Boardly.Dominio.Puertos.CasosDeUso.Empresa;
using Boardly.Dominio.Puertos.Repositorios;
using Boardly.Dominio.Puertos.Repositorios.Cuentas;
using Boardly.Dominio.Utilidades;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace Boardly.Aplicacion.Adaptadores.Empresa;

public class ResultadoPaginaPorEmpleadoIdEmpresa(
    ILogger<ResultadoPaginaPorEmpleadoIdEmpresa> logger,
    IDistributedCache cache,
    IEmpresaRepositorio empresaRepositorio,
    IActividadRepositorio actividadRepositorio,
    ITareaRepositorio tareaRepositorio,
    IEmpleadoRepositorio empleadoRepositorio
    ) : IResultadoPaginaPorEmpleadoIdEmpresa<PaginacionParametro, EmpresaDetallesProyectosDto>
{
   public async Task<ResultadoT<ResultadoPaginado<EmpresaDetallesProyectosDto>>> ObtenerPaginacionPorEmpleadoIdEmpresaAsync(
    Guid empleadoId, 
    PaginacionParametro solicitud,
    CancellationToken cancellationToken)
    {
        if (solicitud.TamanoPagina <= 0 || solicitud.NumeroPagina <= 0)
        {
            logger.LogWarning("Parámetros de paginación inválidos. NúmeroPagina: {NumeroPagina}, TamanoPagina: {TamanoPagina}", 
                solicitud.NumeroPagina, solicitud.TamanoPagina);
            
            return ResultadoT<ResultadoPaginado<EmpresaDetallesProyectosDto>>.Fallo(
                Error.Fallo("400", "Los parámetros de paginación deben ser mayores a cero.")
            );
        }

        var empleado = await empleadoRepositorio.ObtenerByIdAsync(empleadoId, cancellationToken);
        if (empleado is null)
        {
            logger.LogWarning("No se encontró el empleado con ID {EmpleadoId}", empleadoId);
            
            return ResultadoT<ResultadoPaginado<EmpresaDetallesProyectosDto>>.Fallo(
                Error.NoEncontrado("404", "El empleado no existe.")
            );
        }

        var empresaDetallesProyectos =
            await empresaRepositorio.ObtenerEmpresaDetallesPorEmpleadoIdAsync(empleadoId, cancellationToken);

        IEnumerable<Dominio.Modelos.Empresa> detallesProyectos = empresaDetallesProyectos.ToList();

        if (!detallesProyectos.Any())
        {
            logger.LogWarning("El empleado con ID {EmpleadoId} no tiene empresas asociadas.", empleadoId);
            
            return ResultadoT<ResultadoPaginado<EmpresaDetallesProyectosDto>>.Fallo(
                Error.Fallo("404", "El empleado no tiene empresas asociadas.")
            );
        }

        string cacheLlave = $"obtener-empresas-detalles-por-empleado-id-{empleadoId}-{solicitud.NumeroPagina}-{solicitud.TamanoPagina}";

        var proyectoId = detallesProyectos.Select(x =>
        {
            var proyectoId = x.Proyectos.FirstOrDefault()?.ProyectoId ?? Guid.Empty;
            return proyectoId;
        }).First();

        logger.LogInformation("Consultando métricas del proyecto con ID {ProyectoId}", proyectoId);

        var countActividad = await actividadRepositorio.ObtenerNumeroActividadesPorProyectoIdAsync(proyectoId, cancellationToken);
        var countTareasEstado = await tareaRepositorio.ObtenerNumeroDeEstadoDeTareaPorProyectoId(proyectoId, cancellationToken);
        var countTareas = await tareaRepositorio.ObtenerNumeroTareasPorProyectoIdAsync(proyectoId, cancellationToken);

        var resultadoPaginado = await cache.ObtenerOCrearAsync(cacheLlave,
            async () =>
            {
                logger.LogInformation("Construyendo DTOs de empresas para el empleado {EmpleadoId}", empleadoId);

                var empresaDtos = EmpresaMapper.MapearDetallesProyectos(
                    detallesProyectos,
                    empleadoId,
                    countActividad,
                    countTareas,
                    countTareasEstado
                );

                var totalElementos = empresaDtos.Count;

                var elementosPaginados = empresaDtos
                    .Paginar(solicitud.NumeroPagina, solicitud.TamanoPagina)
                    .ToList();

                logger.LogInformation("Paginación completada. Total: {TotalElementos}, Página: {Pagina}, Tamaño: {TamanoPagina}", 
                    totalElementos, solicitud.NumeroPagina, solicitud.TamanoPagina);

                return new ResultadoPaginado<EmpresaDetallesProyectosDto>(
                    elementosPaginados,
                    totalElementos,
                    solicitud.NumeroPagina,
                    solicitud.TamanoPagina
                );
            },
            cancellationToken: cancellationToken
        );

        logger.LogInformation("Consulta exitosa: empresas y proyectos paginados para el empleado {EmpleadoId}", empleadoId);

        return ResultadoT<ResultadoPaginado<EmpresaDetallesProyectosDto>>.Exito(resultadoPaginado);
    }

}