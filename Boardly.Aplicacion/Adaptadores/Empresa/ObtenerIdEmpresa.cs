using Boardly.Aplicacion.DTOs.Empresa;
using Boardly.Dominio.Puertos.CasosDeUso.Empresa;
using Boardly.Dominio.Puertos.Repositorios.Cuentas;
using Boardly.Dominio.Utilidades;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace Boardly.Aplicacion.Adaptadores.Empresa;

public class ObtenerIdEmpresa(
    ILogger<ObtenerIdEmpresa> logger,
    IEmpresaRepositorio empresaRepositorio,
    IDistributedCache cache
    ) : IObtenerIdEmpresa<EmpresaDto>
{
    public async Task<ResultadoT<EmpresaDto>> ObtenerEmpresaIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var empresa = await cache.ObtenerOCrearAsync($"obtener-empresa-id-{id}",
            async () => await empresaRepositorio.ObtenerByIdAsync(id, cancellationToken),
            cancellationToken: cancellationToken
        );

        if (empresa is null)
        {
            logger.LogWarning("No se encontró empresa con ID {EmpresaId}.", id);
            return ResultadoT<EmpresaDto>.Fallo(Error.Fallo("400", $"No existe una empresa con el ID '{id}'."));
        }

        EmpresaDto empresaDto = new
        (
            empresa.EmpresaId,
            empresa.CeoId,
            empresa.Nombre,
            empresa.Descripcion,
            empresa.FechaCreacion,
            empresa.Estado
        );

        logger.LogInformation("Empresa con ID {EmpresaId} obtenida correctamente.", empresa.EmpresaId);

        return ResultadoT<EmpresaDto>.Exito(empresaDto);
    }

}