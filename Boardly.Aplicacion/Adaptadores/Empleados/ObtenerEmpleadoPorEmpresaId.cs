using Boardly.Aplicacion.DTOs.Empleado;
using Boardly.Dominio.Puertos.CasosDeUso.Empleado;
using Boardly.Dominio.Puertos.Repositorios.Cuentas;
using Boardly.Dominio.Utilidades;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace Boardly.Aplicacion.Adaptadores.Empleados;

public class ObtenerEmpleadoPorEmpresaId(
    ILogger<ObtenerEmpleadoPorEmpresaId> logger,
    IEmpleadoRepositorio empleadoRepositorio,
    IEmpresaRepositorio empresaRepositorio,
    IDistributedCache cache
) : IObtenerEmpleadoPorEmpresaId<EmpleadoResumenDto>
{
    public async Task<ResultadoT<IEnumerable<EmpleadoResumenDto>>> ObtenerEmpleadoPorEmpresaIdAsync(Guid empresaId, CancellationToken cancellationToken)
    {
        var empresa = await empresaRepositorio.ObtenerByIdAsync(empresaId, cancellationToken);
        if (empresa is null)
        {
            logger.LogWarning("No se encontró una empresa con el ID: {EmpresaId}", empresaId);
            return ResultadoT<IEnumerable<EmpleadoResumenDto>>.Fallo(
                Error.NoEncontrado("404", "Empresa no encontrada."));
        }

        var empleadosDto = await cache.ObtenerOCrearAsync(
            $"obtener-empleados-dto-por-empresa-{empresaId}",
            async () =>
            {
                var empleados = await empleadoRepositorio.ObtenerPorEmpresaIdAsync(empresaId, cancellationToken);

                return empleados
                    .Select(x => new EmpleadoResumenDto(
                        x.EmpleadoId,
                        x.Usuario.Nombre + " " + x.Usuario?.Apellido,
                        x.Usuario!.Correo,
                        x.EmpleadosProyectoRol.FirstOrDefault()?.RolProyectoId ?? Guid.Empty,
                        x.EmpleadosProyectoRol.FirstOrDefault()?.RolProyecto.Nombre ?? string.Empty,
                        x.EmpleadosProyectoRol.FirstOrDefault()?.ProyectoId ?? Guid.Empty
                    ))
                    .ToList();
            },
            cancellationToken: cancellationToken
        );

        if (!empleadosDto.Any())
        {
            logger.LogWarning("No se encontraron empleados para la empresa con ID: {EmpresaId}", empresaId);

            return ResultadoT<IEnumerable<EmpleadoResumenDto>>.Fallo(
                Error.NoEncontrado("404", "No se encontraron empleados para la empresa."));
        }

        logger.LogInformation("Se encontraron {Cantidad} empleados para la empresa con ID: {EmpresaId}",
            empleadosDto.Count(), empresaId);

        return ResultadoT<IEnumerable<EmpleadoResumenDto>>.Exito(empleadosDto);
    }
}