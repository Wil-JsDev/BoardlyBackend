using Boardly.Aplicacion.DTOs.Proyecto;
using Boardly.Dominio.Modelos;
using Boardly.Dominio.Puertos.CasosDeUso.Proyecto;
using Boardly.Dominio.Puertos.Repositorios;
using Boardly.Dominio.Puertos.Repositorios.Cuentas;
using Boardly.Dominio.Utilidades;
using Microsoft.Extensions.Logging;

namespace Boardly.Aplicacion.Adaptadores.Proyecto;

public class CrearProyecto(
    ILogger<CrearProyecto> logger,
    IProyectoRepositorio repositorioProyecto,
    IEmpresaRepositorio empresaRepositorio,
    IEmpleadoProyectoRolRepositorio empleadoProyectoRolRepositorio,
    IRolProyectoRepositorio rolProyectoRepositorio
    ) : ICrearProyecto<CrearProyectoDto, ProyectoDto>
{
    public async Task<ResultadoT<ProyectoDto>> CrearProyectoAsync(CrearProyectoDto solicitud, CancellationToken cancellationToken)
    {
        if (solicitud is null)
        {
            logger.LogWarning("Solicitud nula recibida al intentar crear un proyecto.");
            
            return ResultadoT<ProyectoDto>.Fallo(Error.Fallo("400", "La solicitud no puede ser nula."));
        }

        var empresa = await empresaRepositorio.ObtenerByIdAsync(solicitud.EmpresaId, cancellationToken);
        if (empresa is null)
        {
            logger.LogWarning("No se encontró la empresa con ID: {EmpresaId}", solicitud.EmpresaId);
            
            return ResultadoT<ProyectoDto>.Fallo(Error.NoEncontrado("404", "La empresa asociada no fue encontrada."));
        }

        if (await repositorioProyecto.ExisteProyectoAsync(solicitud.Nombre, cancellationToken))
        {
            logger.LogWarning("Ya existe un proyecto con el nombre: {Nombre}", solicitud.Nombre);
            
            return ResultadoT<ProyectoDto>.Fallo(Error.Conflicto("409", "Ya existe un proyecto con ese nombre."));
        }
        
        if ( !await ValidacionFecha.ValidarRangoDeFechasAsync(solicitud.FechaInicio, solicitud.FechaFin, cancellationToken) )
        {
            logger.LogWarning("El rango de fechas proporcionado no es válido. FechaInicio: {FechaInicio}, FechaFin: {FechaFin}", 
                solicitud.FechaInicio, solicitud.FechaFin);
    
            return ResultadoT<ProyectoDto>.Fallo(
                Error.Fallo("400", "El rango de fechas no es válido. La fecha de inicio debe ser menor que la fecha de fin y la fecha de inicio debe ser futuras.")
            );
        }

        Dominio.Modelos.Proyecto proyectoEntidad = new()
        {
            ProyectoId = Guid.NewGuid(),
            Nombre = solicitud.Nombre,
            Descripcion = solicitud.Descripcion,
            FechaInicio = solicitud.FechaInicio,
            FechaFin = solicitud.FechaFin,
            Estado = solicitud.Estado.ToString(),
            EmpresaId = solicitud.EmpresaId
        };

        await repositorioProyecto.CrearAsync(proyectoEntidad, cancellationToken);

        // Obtener el rol (usando el proporcionado o buscando "Project Manager" por defecto)
        Guid rolLiderId = solicitud.RolProyectoId 
                         ?? await rolProyectoRepositorio.ObtenerIdPorNombreAsync("Encargado", cancellationToken);

        if (rolLiderId == Guid.Empty)
        {
            logger.LogWarning("No se encontró el rol del proyecto 'Project Manager'.");
            
            return ResultadoT<ProyectoDto>.Fallo(Error.Fallo("400", "El rol del proyecto no fue encontrado."));
        }
        
        // Relación Empleado-Proyecto-Rol
        var empleadoProyectoRol = new EmpleadoProyectoRol
        {
            EmpleadoId = solicitud.EmpleadoId,
            ProyectoId = proyectoEntidad.ProyectoId,
            RolProyectoId = rolLiderId
        };
        
        await empleadoProyectoRolRepositorio.CrearAsync(empleadoProyectoRol, cancellationToken);
        
        logger.LogInformation("Proyecto creado exitosamente. ProyectoId: {ProyectoId}", proyectoEntidad.ProyectoId);
        
        ProyectoDto proyectoDto = new(
            ProyectoId: proyectoEntidad.ProyectoId,
            EmpresaId: proyectoEntidad.EmpresaId,
            Nombre: proyectoEntidad.Nombre,
            Descripcion: proyectoEntidad.Descripcion,
            FechaInicio: proyectoEntidad.FechaInicio,
            FechaFin: proyectoEntidad.FechaFin,
            Estado: proyectoEntidad.Estado,
            FechaCreado: proyectoEntidad.FechaCreado
        );

        return ResultadoT<ProyectoDto>.Exito(proyectoDto);
    }

}