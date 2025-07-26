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

        var conteoDeEmpleadosActivosDeLaEmpresa = await repositorio.ObtenerConteoDeEmpleadosActivosDeLaEmpresaAsync(ceoId, cancellationToken);

        var conteoDeEmpleadosInactivosDeLaEmpresa = await repositorio.ObtenerConteoDeEmpleadosInactivosDeLaEmpresaAsync(ceoId, cancellationToken);

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