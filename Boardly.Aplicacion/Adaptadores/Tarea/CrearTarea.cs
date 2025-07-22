using Boardly.Aplicacion.DTOs.Tarea;
using Boardly.Dominio.Enum;
using Boardly.Dominio.Puertos.CasosDeUso.SignaIR;
using Boardly.Dominio.Puertos.CasosDeUso.Tarea;
using Boardly.Dominio.Puertos.Repositorios;
using Boardly.Dominio.Puertos.Repositorios.Cuentas;
using Boardly.Dominio.Utilidades;
using Microsoft.Extensions.Logging;

namespace Boardly.Aplicacion.Adaptadores.Tarea;

public class CrearTarea(
    ILogger<CrearTarea> logger,
    ITareaRepositorio tareaRepositorio,
    IActividadRepositorio actividadRepositorio,
    INotificadorTareas<TareaDto> notificadorTareas,
    IUsuarioRepositorio usuarioRepositorio
    ) : ICrearTarea<CrearTareaDto, TareaDto>
{
    public async Task<ResultadoT<TareaDto>> CrearTareaAsync(CrearTareaDto solicitud, CancellationToken cancellationToken)
    {
        if ( await tareaRepositorio.ExisteNombreTareaAsync(solicitud.Titulo, solicitud.ProyectoId, cancellationToken) )
        {
            logger.LogWarning("Ya existe una tarea con el nombre {Titulo} en el proyecto {ProyectoId}", solicitud.Titulo, solicitud.ProyectoId);

            return ResultadoT<TareaDto>.Fallo(Error.Conflicto("409", "Ya existe una tarea con el nombre especificado."));
        }

        var usuario = await usuarioRepositorio.ObtenerByIdAsync(solicitud.UsuarioId, cancellationToken);
        if (usuario is null)
        {
            logger.LogWarning("El usuario {UsuarioId} no fue encontrado", solicitud.UsuarioId);
            
            return ResultadoT<TareaDto>.Fallo(Error.NoEncontrado("404", "El usuario especificado no fue encontrado."));
        }
        
        var actividad = await actividadRepositorio.ObtenerByIdAsync(solicitud.ActividadId, cancellationToken);
        if (actividad is null)
        {
            logger.LogWarning("La actividad {ActividadId} no fue encontrada", solicitud.ActividadId);
            
            return ResultadoT<TareaDto>.Fallo(Error.NoEncontrado("404", "La actividad especificada no fue encontrada."));
        }
        
        if (!await ValidacionFecha.ValidarRangoDeFechasAsync(solicitud.FechaInicio, solicitud.FechaVencimiento, cancellationToken))
        {
            logger.LogWarning("El rango de fechas proporcionado no es válido. FechaInicio: {FechaInicio}, FechaFin: {FechaVencimiento}", 
                solicitud.FechaInicio, solicitud.FechaVencimiento);
    
            return ResultadoT<TareaDto>.Fallo(
                Error.Fallo("400", "El rango de fechas no es válido. La fecha de inicio debe ser menor que la fecha de fin y la fecha de inicio debe ser futuras.")
            );
        }

        Dominio.Modelos.Tarea tareaEntidad = new()
        {
            TareaId = Guid.NewGuid(),
            ProyectoId = solicitud.ProyectoId,
            Titulo = solicitud.Titulo,
            Descripcion = solicitud.Descripcion,
            Estado = nameof(EstadoTarea.Pendiente),
            FechaInicio = solicitud.FechaInicio,
            FechaVencimiento = solicitud.FechaVencimiento,
            FechaActualizacion = DateTime.UtcNow,
            FechaCompletada = null,
            ActividadId = solicitud.ActividadId
        };

        logger.LogInformation("Creando tarea {TareaId} en el proyecto {ProyectoId}", tareaEntidad.TareaId, tareaEntidad.ProyectoId);
        
        await tareaRepositorio.CrearAsync(tareaEntidad, cancellationToken);
        
        TareaDto tareaCreadaDto = new
        (
            TareaId: tareaEntidad.TareaId,
            ProyectoId: tareaEntidad.ProyectoId,
            Titulo: tareaEntidad.Titulo,
            EstadoTarea: tareaEntidad.Estado,
            Descripcion: tareaEntidad.Descripcion,
            FechaInicio: tareaEntidad.FechaInicio, 
            FechaVencimiento: tareaEntidad.FechaVencimiento,
            FechaActualizacion:tareaEntidad.FechaActualizacion,  
            FechaCreado: tareaEntidad.FechaCreado,
            ActividadId: solicitud.ActividadId
        );
        
        await notificadorTareas.NotificarNuevaTarea(solicitud.UsuarioId, tareaCreadaDto);
        
        return ResultadoT<TareaDto>.Exito(tareaCreadaDto);
    }
}