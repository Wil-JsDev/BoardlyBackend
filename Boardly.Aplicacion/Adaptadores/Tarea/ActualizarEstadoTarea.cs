using Boardly.Aplicacion.DTOs.Tarea;
using Boardly.Aplicacion.DTOs.Usuario;
using Boardly.Dominio.Enum;
using Boardly.Dominio.Puertos.CasosDeUso.SignaIR;
using Boardly.Dominio.Puertos.CasosDeUso.Tarea;
using Boardly.Dominio.Puertos.Repositorios;
using Boardly.Dominio.Utilidades;
using Microsoft.Extensions.Logging;

namespace Boardly.Aplicacion.Adaptadores.Tarea;

public class ActualizarEstadoTarea(
    ILogger<ActualizarEstadoTarea> logger,
    ITareaRepositorio tareaRepositorio,
    INotificadorTareas<TareaDto> notificador
    ) : IActualizarEstadoTarea
{
    public async Task<Resultado> CambiarEstadoAsync(Guid tareaId, EstadoTarea nuevoEstado, CancellationToken cancellationToken)
    {
        var tarea = await tareaRepositorio.ObtenerConEmpleadosAsync(tareaId, cancellationToken);
        if (tarea is null)
        {
            logger.LogWarning("");
            
            return Resultado.Fallo(Error.NoEncontrado("404", "Este ID de tarea no existe."));
        }
        
        tarea.Estado = nuevoEstado.ToString();
        tarea.FechaActualizacion = DateTime.UtcNow;

        if (nuevoEstado == EstadoTarea.Finalizada)
        {
            logger.LogInformation("Tarea con ID {TareaId} finalizada exitosamente.", tarea.TareaId);
            
            tarea.FechaCompletada = DateTime.UtcNow;
            
            await tareaRepositorio.ActualizarAsync(tarea, cancellationToken);
            
            return Resultado.Exito();
        }
        
        await tareaRepositorio.ActualizarAsync(tarea, cancellationToken);
        
        TareaDto tareaDto = new(
            tarea.TareaId,
            tarea.ProyectoId,
            tarea.Titulo,
            tarea.Estado,
            tarea.Descripcion,
            tarea.FechaInicio,
            tarea.FechaVencimiento,
            tarea.FechaActualizacion,
            tarea.FechaCreado,
            tarea.ActividadId,
            UsuarioFotoPerfil: tarea.TareasEmpleado
                .Select(te => new UsuarioFotoPerfilDto(
                    UsuarioId: te.Empleado!.UsuarioId,
                    FotoPerfil: te.Empleado!.Usuario.FotoPerfil
                ))
                .ToList()
        );
        
        var empleadoId = tarea.TareasEmpleado!.FirstOrDefault()?.EmpleadoId ?? Guid.Empty;;

        await NuevoEstadoTarea(nuevoEstado, empleadoId, tareaDto);

        logger.LogInformation("");
        
        return Resultado.Exito();
    }

    private async Task NuevoEstadoTarea(EstadoTarea nuevoEstado, Guid empleadoId, TareaDto tareaDto)
    {
        await (nuevoEstado switch
        {
            EstadoTarea.Pendiente => notificador.NotificarTareaEnPendiente(empleadoId, tareaDto),
            EstadoTarea.EnProceso => notificador.NotificarTareaEnProceso(empleadoId, tareaDto),
            EstadoTarea.EnRevision => notificador.NotificarTareaEnRevision(empleadoId, tareaDto),
            EstadoTarea.Finalizada => notificador.NotificarTareaFinalizada(empleadoId, tareaDto)

        });

    }
    
}