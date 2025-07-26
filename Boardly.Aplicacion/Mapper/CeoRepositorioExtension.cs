using Boardly.Aplicacion.DTOs.Ceo;
using Boardly.Dominio.Puertos.Repositorios.Cuentas;
using Boardly.Dominio.Utilidades;
using Microsoft.Extensions.Logging;

namespace Boardly.Aplicacion.Mapper;

public static class CeoRepositorioExtension
{
    public static async Task<ResultadoT<ConteoEmpleadosCeoDto>> MapearConteoEmpleadoCeoAsync(
        this ICeoRepositorio repositorio,
        Guid ceoId,
        ILogger logger,
        CancellationToken cancellationToken)
    {
        
        var conteoDeEmpleadosDentroDeLaEmpresa = await repositorio.ObtenerConteoDeEmpleadosDentroDeLaEmpresaAsync(ceoId, cancellationToken);
        if (conteoDeEmpleadosDentroDeLaEmpresa == 0)
        {
            logger.LogWarning("El CEO con ID {CeoId} no tiene empleados asignados.", ceoId);
            
            return ResultadoT<ConteoEmpleadosCeoDto>.Fallo(Error.NoEncontrado("404", "El CEO no tiene empleados asignados."));
        }

        var conteoDeEmpleadosActivosDeLaEmpresa = await repositorio.ObtenerConteoDeEmpleadosActivosDeLaEmpresaAsync(ceoId, cancellationToken);
        if (conteoDeEmpleadosActivosDeLaEmpresa == 0)
        {
            logger.LogWarning("El CEO con ID {CeoId} no tiene empleados activos asignados.", ceoId);
            
            return ResultadoT<ConteoEmpleadosCeoDto>.Fallo(Error.NoEncontrado("404", "El CEO no tiene empleados activos asignados."));
        }

        var conteoDeEmpleadosInactivosDeLaEmpresa = await repositorio.ObtenerConteoDeEmpleadosInactivosDeLaEmpresaAsync(ceoId, cancellationToken);
        if (conteoDeEmpleadosInactivosDeLaEmpresa == 0)
        {
            logger.LogWarning("El CEO con ID {CeoId} no tiene empleados inactivos asignados.", ceoId);
            
            return ResultadoT<ConteoEmpleadosCeoDto>.Fallo(Error.NoEncontrado("404", "El CEO no tiene empleados inactivos asignados."));
        }

        var conteoDto = new ConteoEmpleadosCeoDto
        (
            TotalEmpleados: conteoDeEmpleadosDentroDeLaEmpresa,
            EmpleadosActivos: conteoDeEmpleadosActivosDeLaEmpresa,
            EmpleadosInactivos: conteoDeEmpleadosInactivosDeLaEmpresa
        );

        logger.LogInformation("Conteo de empleados de la empresa obtenido correctamente.");
        
        return ResultadoT<ConteoEmpleadosCeoDto>.Exito(conteoDto);
    }
}