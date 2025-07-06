using Boardly.Aplicacion.DTOs.Empleado;
using Boardly.Dominio.Puertos.CasosDeUso.Empleado;
using Boardly.Dominio.Puertos.Repositorios.Cuentas;
using Boardly.Dominio.Utilidades;
using Microsoft.Extensions.Logging;

namespace Boardly.Aplicacion.Adaptadores.Empleados;

public class ObtenerIdEmpleado(
    ILogger<ObtenerIdEmpleado> logger,
    IEmpleadoRepositorio empleadoRepositorio
    ) : IObtenerIdEmpleado<EmpleadoDto>
{
    public async Task<ResultadoT<EmpleadoDto>> ObtenerIdEmpleadoAsync(Guid id, CancellationToken cancellationToken)
    {
        var empleado = await empleadoRepositorio.ObtenerByIdAsync(id, cancellationToken);
        if (empleado is null)
        {
            logger.LogWarning("No se encontró ningún empleado con el ID proporcionado: {EmpleadoId}", id);
            
            return ResultadoT<EmpleadoDto>.Fallo(Error.Fallo("400", "Empleado no encontrado con el ID especificado."));
        }   
        
        EmpleadoDto empleadoDto = new
        (
            EmpleadoId: id, 
            UsuarioId:  empleado.UsuarioId,
            EmpresaId: empleado.EmpresaId
        );

        logger.LogInformation("Empleado encontrado exitosamente. ID: {EmpleadoId}, UsuarioID: {UsuarioId}", 
            empleadoDto.EmpleadoId, empleadoDto.UsuarioId);
        
        return ResultadoT<EmpleadoDto>.Exito(empleadoDto);
    }
}