using Boardly.Aplicacion.DTOs.Empleado;
using Boardly.Aplicacion.Mapper;
using Boardly.Dominio.Puertos.CasosDeUso.Empleado;
using Boardly.Dominio.Puertos.Repositorios.Cuentas;
using Boardly.Dominio.Utilidades;
using Microsoft.Extensions.Logging;

namespace Boardly.Aplicacion.Adaptadores.Empleados;

public class ObtenerEstadisticasEnConteoEmpleado(
    ILogger<ObtenerEstadisticasEnConteoEmpleado> logger,
    IEmpleadoRepositorio empleadoRepositorio
    ) : IObtenerEstadisticasEnConteoEmpleado<ConteoEmpleadoDto>
{
    public async Task<ResultadoT<ConteoEmpleadoDto>> ObtenerEstadisticasEnConteoEmpleadoAsync(Guid empleadoId, CancellationToken cancellationToken)
    {
        var empleado = await empleadoRepositorio.ObtenerByIdAsync(empleadoId, cancellationToken);
        if (empleado is null) 
        {
            logger.LogWarning("No se encontró un empleado con el ID {EmpleadoId}.", empleadoId);

            return ResultadoT<ConteoEmpleadoDto>.Fallo(
                Error.NoEncontrado("404", $"No se encontró un empleado con el ID {empleadoId}."));
        }
        
        var resultado = await empleadoRepositorio.MapearConteoEmpleadoAsync(empleadoId, logger, cancellationToken);

        return resultado;
    }
}