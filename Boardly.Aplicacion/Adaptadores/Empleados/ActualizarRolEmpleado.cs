using Boardly.Aplicacion.DTOs.Empleado;
using Boardly.Dominio.Puertos.CasosDeUso.Empleado;
using Boardly.Dominio.Puertos.Repositorios;
using Boardly.Dominio.Puertos.Repositorios.Cuentas;
using Boardly.Dominio.Utilidades;
using Microsoft.Extensions.Logging;

namespace Boardly.Aplicacion.Adaptadores.Empleados;

public class ActualizarRolEmpleado(
    ILogger<ActualizarRolEmpleado> logger,
    IEmpleadoRepositorio empleadoRepositorio,
    IRolProyectoRepositorio rolProyectoRepositorio
    ) : IActualizarRolEmpleado<ActualizarRolEmpleadoDto,EmpleadoResumenDto>
{
    public async Task<ResultadoT<EmpleadoResumenDto>> ActualizarRolEmpleadoAsync(Guid empleadoId, ActualizarRolEmpleadoDto solicitud, CancellationToken cancellationToken)
    {
        if (empleadoId == Guid.Empty || solicitud.rolProyectoId == Guid.Empty)
        {
            logger.LogWarning(" El ID del empleado o del rol está vacío.");
            return ResultadoT<EmpleadoResumenDto>.Fallo(Error.Fallo(
                "400",
                "El identificador del empleado o del rol no puede estar vacío."
            ));
        }

        var empleado = await empleadoRepositorio.ObtenerEmpleadoByIdAsync(empleadoId, cancellationToken);
        if (empleado is null)
        {
            logger.LogWarning("No se encontró el empleado con ID {EmpleadoId}.", empleadoId);
            return ResultadoT<EmpleadoResumenDto>.Fallo(Error.NoEncontrado(
                "404",
                $"No existe un empleado con el ID {empleadoId}."
            ));
        }

        var rolProyecto = await rolProyectoRepositorio.ObtenerByIdAsync(solicitud.rolProyectoId, cancellationToken);
        if (rolProyecto is null)
        {
            logger.LogWarning("No se encontró el rol con ID {RolId}.", solicitud.rolProyectoId);
            return ResultadoT<EmpleadoResumenDto>.Fallo(Error.NoEncontrado(
                "404",
                $"No existe un rol con el ID {solicitud.rolProyectoId}."
            ));
        }

        var relacion = empleado.EmpleadosProyectoRol
            .FirstOrDefault(epr => epr.ProyectoId == solicitud.proyectoId);

        if (relacion == null)
        {
            logger.LogWarning("No se encontró una relación entre el empleado {EmpleadoId} y el proyecto {ProyectoId}.", empleadoId, solicitud.proyectoId);
            return ResultadoT<EmpleadoResumenDto>.Fallo(Error.Fallo(
                "400",
                "No se encontró una relación del empleado con el proyecto especificado."
            ));
        }
        
        relacion.RolProyectoId = solicitud.rolProyectoId;

        logger.LogInformation("Rol actualizado correctamente para el empleado con ID {EmpleadoId}.", empleadoId);
        
        await empleadoRepositorio.ActualizarAsync(empleado, cancellationToken);

        EmpleadoResumenDto empleadoResumenDto = new(
            EmpleadoId: empleado.EmpleadoId,
            Nombre: $"{empleado.Usuario.Nombre} {empleado.Usuario.Apellido}".Trim(),
            Correo: empleado.Usuario.Correo,
            RolId: rolProyecto.RolProyectoId,
            NombreRol: rolProyecto.Nombre,
            ProyectoId: relacion.ProyectoId
        );
        


        return ResultadoT<EmpleadoResumenDto>.Exito(empleadoResumenDto);
    }
    
}
