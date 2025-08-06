using Boardly.Aplicacion.DTOs.Empleado;
using Boardly.Dominio.Modelos;
using Boardly.Dominio.Puertos.CasosDeUso.Empleado;
using Boardly.Dominio.Puertos.Repositorios;
using Boardly.Dominio.Puertos.Repositorios.Cuentas;
using Boardly.Dominio.Utilidades;
using Microsoft.Extensions.Logging;

namespace Boardly.Aplicacion.Adaptadores.Empleados;

public class AgregarEmpleadoProyecto(
    ILogger<AgregarEmpleadoProyecto> logger,
    IProyectoRepositorio proyectoRepositorio,
    IEmpleadoRepositorio empleadoRepositorio
) : IAgregarEmpleadoProyecto<AgregarEmpleadoProyectoDto, EmpleadoResumenDto>
{
    public async Task<ResultadoT<EmpleadoResumenDto>> AgregarEmpleadoProyectoAsync(Guid empleadoId, AgregarEmpleadoProyectoDto solicitud, CancellationToken cancellationToken)
    {
        if (empleadoId == Guid.Empty)
        {
            logger.LogWarning("AgregarEmpleadoProyectoAsync: El ID del empleado o proyecto está vacío.");
            return ResultadoT<EmpleadoResumenDto>.Fallo(Error.Fallo(
                "400",
                "El ID del empleado o del proyecto no puede estar vacío."
            ));
        }

        var empleado = await empleadoRepositorio.ObtenerEmpleadoByIdAsync(empleadoId, cancellationToken);
        if (empleado is null)
        {
            logger.LogWarning("Empleado con ID {EmpleadoId} no encontrado.", empleadoId);
            return ResultadoT<EmpleadoResumenDto>.Fallo(Error.NoEncontrado("404", $"No existe el empleado con ID {empleadoId}."));
        }

        var proyecto = await proyectoRepositorio.ObtenerProyectoEmpleadosPorIdAsync(solicitud.proyectoId, cancellationToken);
        if (proyecto is null)
        {
            logger.LogWarning("Proyecto con ID {ProyectoId} no encontrado.", solicitud.proyectoId);
            return ResultadoT<EmpleadoResumenDto>.Fallo(Error.NoEncontrado("404", $"No existe el proyecto con ID {solicitud.proyectoId}."));
        }

        var yaAsignado = proyecto.EmpleadosProyectoRol.Any(epr => epr.EmpleadoId == empleadoId);
        if (!yaAsignado)
        {
            proyecto.EmpleadosProyectoRol.Add(new EmpleadoProyectoRol
            {
                EmpleadoId = empleadoId,
                ProyectoId = solicitud.proyectoId,
                RolProyectoId = solicitud.rolProyectoId
            });

            await proyectoRepositorio.ActualizarAsync(proyecto, cancellationToken);

            logger.LogInformation("Empleado {EmpleadoId} agregado al proyecto {ProyectoId} correctamente.", empleadoId, solicitud.proyectoId);
        }
        else
        {        
            logger.LogInformation("Empleado {EmpleadoId} ya estaba asignado al proyecto {ProyectoId}.", empleadoId, solicitud.proyectoId);
            return ResultadoT<EmpleadoResumenDto>.Fallo(Error.Fallo("400","El empleado ya ha sido asignado al proyecto"));
            
        }
        var empleadoProyectoRol = empleado.EmpleadosProyectoRol.FirstOrDefault(r => r.ProyectoId == solicitud.proyectoId);

     
        var dto = new EmpleadoResumenDto(
            EmpleadoId: empleado.EmpleadoId,
            Nombre: $"{empleado.Usuario.Nombre} {empleado.Usuario.Apellido}".Trim(),
            Correo: empleado.Usuario.Correo,
            RolId: empleadoProyectoRol?.RolProyectoId ?? Guid.Empty,
            NombreRol: empleadoProyectoRol?.RolProyecto?.Nombre ?? string.Empty,
            ProyectoId: solicitud.proyectoId
        );


        return ResultadoT<EmpleadoResumenDto>.Exito(dto);
    }
}
