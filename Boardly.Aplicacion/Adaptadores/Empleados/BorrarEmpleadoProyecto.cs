using Boardly.Aplicacion.DTOs.Empleado;
using Boardly.Dominio.Puertos.CasosDeUso.Empleado;
using Boardly.Dominio.Puertos.Repositorios;
using Boardly.Dominio.Puertos.Repositorios.Cuentas;
using Boardly.Dominio.Utilidades;
using Microsoft.Extensions.Logging;

namespace Boardly.Aplicacion.Adaptadores.Empleados;

public class BorrarEmpleadoProyecto(
    ILogger<BorrarEmpleadoProyecto> logger,
    IEmpleadoRepositorio empleadoRepositorio,
    IProyectoRepositorio proyectoRepositorio
    ): IBorrarEmpleadoProyecto
{
    public async Task<ResultadoT<string>> BorrarEmpleadoProyectoAsync(Guid empleadoId, Guid proyectoId, CancellationToken cancellationToken)
    {
        if (empleadoId == Guid.Empty || proyectoId == Guid.Empty)
        {
            logger.LogWarning("El ID del empleado o proyecto está vacío.");
            return ResultadoT<string>.Fallo(Error.Fallo(
                "400",
                "El ID del empleado y del proyecto no pueden estar vacíos."
            ));
        }
    
        var empleado = await empleadoRepositorio.ObtenerEmpleadoByIdAsync(empleadoId, cancellationToken);
        if (empleado is null)
        {
            logger.LogWarning("Empleado con ID {EmpleadoId} no encontrado.", empleadoId);
            return ResultadoT<string>.Fallo(Error.NoEncontrado("404", $"No se encontró al empleado con ID {empleadoId}."));
        }
    
        var proyecto = await proyectoRepositorio.ObtenerByIdAsync(proyectoId, cancellationToken);
        if (proyecto is null)
        {
            logger.LogWarning("Proyecto con ID {ProyectoId} no encontrado.", proyectoId);
            return ResultadoT<string>.Fallo(Error.NoEncontrado("404", $"No se encontró el proyecto con ID {proyectoId}."));
        }
    
        var eliminar = await empleadoRepositorio.BorrarEmpleadoDeUnProyectoAsync(empleadoId, proyectoId, cancellationToken);

        if (eliminar == 0)
        {
            logger.LogWarning("El empleado con ID {EmpleadoId} no está asignado al proyecto con ID {ProyectoId}.", empleadoId, proyectoId);
            return ResultadoT<string>.Fallo(Error.Fallo("404", "El empleado no está asignado a ese proyecto o ya fue removido."));
        }
    
        return ResultadoT<string>.Exito("Empleado removido correctamente del proyecto.");
    }

}