using Boardly.Aplicacion.DTOs.Actividad;
using Boardly.Dominio.Puertos.CasosDeUso.Actividad;
using Boardly.Dominio.Puertos.Repositorios;
using Boardly.Dominio.Utilidades;
using Microsoft.Extensions.Logging;

namespace Boardly.Aplicacion.Adaptadores.Actividad;

public class CrearActividad(
    ILogger<CrearActividad> logger,
    IActividadRepositorio actividadRepositorio,
    IProyectoRepositorio proyectoRepositorio
    ) : ICrearActividad<CrearActividaDto, ActividadDto>
{
    public async Task<ResultadoT<ActividadDto>> CrearActividadAsync(CrearActividaDto solicitud, CancellationToken cancellationToken)
    {
        if (solicitud is null)
        {
            logger.LogWarning("Se recibió una solicitud nula al intentar crear una actividad.");
        
            return ResultadoT<ActividadDto>.Fallo(
                Error.Fallo("400", "La solicitud no puede ser nula."));
        }
        
        var proyecto = await proyectoRepositorio.ObtenerByIdAsync(solicitud.ProyectoId, cancellationToken);
        if (proyecto is null)
        {
            logger.LogWarning("El id del proyecto {ProyectoId} no fue encontrado", proyecto!.ProyectoId);
            
            return ResultadoT<ActividadDto>.Fallo(Error.NoEncontrado("404", "El proyecto especificado no fue encontrado."));
        }

        if (await actividadRepositorio.ExisteNombreActividadAsync(solicitud.Nombre, cancellationToken))
        {
            logger.LogWarning("Ya existe una actividad con el nombre: {Nombre}", solicitud.Nombre);
        
            return ResultadoT<ActividadDto>.Fallo(
                Error.Conflicto("409", "Ya existe una actividad con ese nombre."));
        }

        if ( !await ValidacionFecha.ValidarRangoDeFechasAsync(solicitud.FechaInicio, solicitud.FechaFin, cancellationToken) )
        {
            logger.LogWarning("El rango de fechas proporcionado no es válido. FechaInicio: {FechaInicio}, FechaFin: {FechaFin}", 
                solicitud.FechaInicio, solicitud.FechaFin);
    
            return ResultadoT<ActividadDto>.Fallo(
                Error.Fallo("400", "El rango de fechas no es válido. La fecha de inicio debe ser menor que la fecha de fin y la fecha de inicio debe ser futuras.")
            );
        }

        Dominio.Modelos.Actividad actividadEntidad = new()
        {
            ActividadId = Guid.NewGuid(),
            ProyectoId = solicitud.ProyectoId,
            Nombre = solicitud.Nombre,
            Prioridad = solicitud.Prioridad.ToString(), 
            Descripcion = solicitud.Descripcion,
            Estado = solicitud.Estado.ToString(),       
            FechaInicio = solicitud.FechaInicio,
            FechaFinalizacion = solicitud.FechaFin,
            Orden = solicitud.Orden
        };

        await actividadRepositorio.CrearAsync(actividadEntidad, cancellationToken);
    
        logger.LogInformation("Actividad creada exitosamente con ID: {ActividadId}", actividadEntidad.ActividadId);
    
        ActividadDto actividadDto = new
        (
            actividadEntidad.ActividadId,
            actividadEntidad.ProyectoId,
            actividadEntidad.Nombre!,
            actividadEntidad.Prioridad!,
            actividadEntidad.Descripcion!,
            actividadEntidad.Estado!,
            actividadEntidad.FechaInicio,
            actividadEntidad.FechaFinalizacion,
            actividadEntidad.Orden
        );
    
        return ResultadoT<ActividadDto>.Exito(actividadDto);
    }

}