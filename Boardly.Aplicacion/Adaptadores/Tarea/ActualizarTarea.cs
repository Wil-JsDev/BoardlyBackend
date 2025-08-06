using Boardly.Aplicacion.DTOs.Tarea;
using Boardly.Aplicacion.DTOs.Usuario;
using Boardly.Dominio.Puertos.CasosDeUso.SignaIR;
using Boardly.Dominio.Puertos.CasosDeUso.Tarea;
using Boardly.Dominio.Puertos.Repositorios;
using Boardly.Dominio.Puertos.Repositorios.Cuentas;
using Boardly.Dominio.Utilidades;
using Microsoft.Extensions.Logging;

namespace Boardly.Aplicacion.Adaptadores.Tarea;

public class ActualizarTarea(
    ILogger<ActualizarTarea> logger,
    ITareaRepositorio tareaRepositorio,
    IUsuarioRepositorio usuarioRepositorio,
    ITareaEmpleadoRepositorio tareaEmpleadoRepositorio,
    INotificadorTareas<TareaDto> notificadorTareas
    ) : IActualizarTarea<ActualizarTituloTareaDto, TareaDto>
{
   public async Task<ResultadoT<TareaDto>> ActualizarTareaAsync(Guid tareaId, ActualizarTituloTareaDto solicitud, CancellationToken cancellationToken)
    {
        var tarea = await tareaRepositorio.ObtenerByIdAsync(tareaId, cancellationToken);
        if (tarea is null)
        {
            logger.LogWarning("No se encontró la tarea con ID: {TareaId}", tareaId);
            
            return ResultadoT<TareaDto>.Fallo(Error.NoEncontrado("404", "Este ID de tarea no existe."));
        }
        
        var usuario = await usuarioRepositorio.ObtenerByIdAsync(solicitud.UsuarioId, cancellationToken);
        if (usuario is null)
        {
            logger.LogWarning("No se encontró el usuario con ID: {UsuarioId}", solicitud.UsuarioId);
            
            return ResultadoT<TareaDto>.Fallo(Error.NoEncontrado("404", "Este ID de usuario no existe."));
        }
        
        if (solicitud.EmpleadoIds.Count == 0)
        {
            logger.LogWarning("No se proporcionaron empleados para actualizar la tarea con ID: {TareaId}", tareaId);
            
            return ResultadoT<TareaDto>.Fallo(Error.Fallo("400", "Debe proporcionar al menos un empleado para asignar la tarea."));
        }
        
        tarea.Titulo = solicitud.Titulo;
        tarea.Descripcion = solicitud.Descripcion;
        tarea.FechaActualizacion = DateTime.UtcNow;

        await tareaRepositorio.ActualizarAsync(tarea, cancellationToken);
        
        await tareaEmpleadoRepositorio.ActualizarTareasEmpleadosAsync(tareaId, solicitud.EmpleadoIds, cancellationToken);
        
        logger.LogInformation("Tarea con ID {TareaId} actualizada exitosamente. Nuevo título: '{NuevoTitulo}'", 
            tareaId, tarea.Titulo);
        
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
                    UsuarioId: te.Empleado?.UsuarioId,
                    FotoPerfil: te.Empleado?.Usuario.FotoPerfil
                )).ToList(),
        tarea.Archivo
        );

        await notificadorTareas.NotificarTareaActualizada(solicitud.UsuarioId, tareaDto);

        return ResultadoT<TareaDto>.Exito(tareaDto);
    }
   
}