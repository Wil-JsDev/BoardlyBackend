using Boardly.Aplicacion.DTOs.Actividad;
using Boardly.Dominio.Puertos.CasosDeUso.Actividad;
using Boardly.Dominio.Puertos.Repositorios;
using Boardly.Dominio.Utilidades;
using Microsoft.Extensions.Logging;

namespace Boardly.Aplicacion.Adaptadores.Actividad;

public class ActualizarActividad(
    ILogger<ActualizarActividad> logger,
    IActividadRepositorio actividadRepositorio
    ) : IActualizarActividad<ActualizarActividadDto, ActividadDto>
{
    public async Task<ResultadoT<ActividadDto>> ActualizarActividadAsync(Guid id, ActualizarActividadDto solicitud, CancellationToken cancellationToken)
    {
        var actividad = await actividadRepositorio.ObtenerByIdAsync(id, cancellationToken);

        if (actividad is null)
        {
            logger.LogWarning("No se encontró la actividad con ID: {ActividadId}", id);

            return ResultadoT<ActividadDto>.Fallo(
                Error.NoEncontrado("404", "La actividad especificada no fue encontrada."));
        }

        if (await actividadRepositorio.NombreActividadEnUso(solicitud.Nombre, id, cancellationToken))
        {
            logger.LogWarning("Ya existe una actividad con el nombre: {Nombre}", solicitud.Nombre);
            
            return ResultadoT<ActividadDto>.Fallo(
                Error.Conflicto("409", "Ya existe una actividad con ese nombre."));
        }

        actividad.Nombre = solicitud.Nombre;
        actividad.Descripcion = solicitud.Descripcion;
        actividad.Estado = nameof(solicitud.Estado);
        actividad.FechaInicio = solicitud.FechaInicio;
        actividad.FechaFinalizacion = solicitud.FechaFin;
        
        await actividadRepositorio.ActualizarAsync(actividad, cancellationToken);
        
        logger.LogInformation("Actividad actualizada exitosamente. ID: {ActividadId}", actividad.ActividadId);

        ActividadDto actividadDto = new
        (
            actividad.ActividadId,
            actividad.ProyectoId,       
            actividad.Nombre!,
            actividad.Prioridad!, 
            actividad.Descripcion!,
            actividad.Estado!,
            actividad.FechaInicio,
            actividad.FechaFinalizacion,
            0 
        );
        
        return ResultadoT<ActividadDto>.Exito(actividadDto);

    }
}