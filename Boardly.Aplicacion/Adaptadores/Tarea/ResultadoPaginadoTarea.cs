using Boardly.Aplicacion.DTOs.Paginacion;
using Boardly.Aplicacion.DTOs.Tarea;
using Boardly.Aplicacion.DTOs.Usuario;
using Boardly.Dominio.Helper;
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
            
            var resultadoPagina = await repositorioTarea.ObtenerPaginadoTareaAsync(
                actividadId,
                solicitud.NumeroPagina,
                solicitud.TamanoPagina,
                cancellationToken);

            if (resultadoPagina.Elementos == null)
            {
                logger.LogError("No se pudieron obtener las tareas paginadas para la actividad {ActividadId}", actividadId);

                return ResultadoT<ResultadoPaginado<TareaDto>>.Fallo(
                    Error.NoEncontrado("404", "No se pudo obtener las tareas paginadas."));
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
                ActividadId: x.ActividadId,
                UsuarioFotoPerfil: x.TareasEmpleado.Select(te => new UsuarioFotoPerfilDto(
                    UsuarioId: te.Empleado!.UsuarioId,
                    FotoPerfil: te.Empleado!.Usuario.FotoPerfil
                )).ToList()
            )).ToList();
        
            var totalElementos = resultadoPaginaDto.Count;
        
            var elementosPaginados = resultadoPaginaDto
                .Paginar(solicitud.NumeroPagina, solicitud.TamanoPagina)
                .ToList();
        
            var resultadoPaginado = new ResultadoPaginado<TareaDto>(
                elementos: elementosPaginados,
                totalElementos: totalElementos,
                paginaActual: solicitud.NumeroPagina,
                tamanioPagina: solicitud.TamanoPagina
            );
   
            
            logger.LogInformation("Paginación de tareas para actividad {ActividadId}: Página {Pagina}, Tamaño {Tamano}. Tareas obtenidas: {Cantidad}",
                actividadId, solicitud.NumeroPagina, solicitud.TamanoPagina, resultadoPaginado.Elementos!.Count());

            return ResultadoT<ResultadoPaginado<TareaDto>>.Exito(resultadoPaginado);
       }
}