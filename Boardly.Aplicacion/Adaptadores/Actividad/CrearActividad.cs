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
            logger.LogWarning("");
            
            return ResultadoT<ActividadDto>.Fallo(Error.NoEncontrado("404", "El proyecto especificado no fue encontrado."));
        }

        if (await actividadRepositorio.ExisteNombreActividadAsync(solicitud.Nombre, cancellationToken))
        {
            logger.LogWarning("Ya existe una actividad con el nombre: {Nombre}", solicitud.Nombre);
        
            return ResultadoT<ActividadDto>.Fallo(
                Error.Conflicto("409", "Ya existe una actividad con ese nombre."));
        }

        Dominio.Modelos.Actividad actividadEntidad = new()
        {
            ActividadId = Guid.NewGuid(),
            ProyectoId = solicitud.ProyectoId,
            Nombre = solicitud.Nombre,
            Prioridad = nameof(solicitud.Prioridad), 
            Descripcion = solicitud.Descripcion,
            Estado = nameof(solicitud.Estado),       
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