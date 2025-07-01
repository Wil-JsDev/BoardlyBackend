using Boardly.Dominio.Puertos.CasosDeUso.Empleado;
using Boardly.Dominio.Puertos.Repositorios.Cuentas;
using Boardly.Dominio.Utilidades;
using Microsoft.Extensions.Logging;

namespace Boardly.Aplicacion.Adaptadores.Empleados;

public class BorrarEmpleado(
    ILogger<BorrarEmpleado> logger,
    IEmpleadoRepositorio empleadoRepositorio
    ) : IBorrarEmpleado
{
    public async Task<ResultadoT<Guid>> BorrarEmpleadoAsync(Guid id, CancellationToken cancellationToken)
    {
        var empleado = await empleadoRepositorio.ObtenerByIdAsync(id, cancellationToken);
        if (empleado is null)
        {
            logger.LogWarning("No se encontró ningún empleado con el ID proporcionado: {EmpleadoId}", id);
            
            return ResultadoT<Guid>.Fallo(Error.Fallo("400", "Empleado no encontrado con el ID especificado."));
        }        
    
        await empleadoRepositorio.EliminarAsync(empleado, cancellationToken);
    
        logger.LogInformation("Empleado eliminado exitosamente. ID: {EmpleadoId}", empleado.EmpleadoId);
    
        return ResultadoT<Guid>.Exito(empleado.EmpleadoId);
    }

}