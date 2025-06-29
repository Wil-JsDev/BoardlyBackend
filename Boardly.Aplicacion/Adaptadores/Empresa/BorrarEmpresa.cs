using Boardly.Dominio.Puertos.CasosDeUso.Empresa;
using Boardly.Dominio.Puertos.Repositorios.Cuentas;
using Boardly.Dominio.Utilidades;
using Microsoft.Extensions.Logging;

namespace Boardly.Aplicacion.Adaptadores.Empresa;

public class BorrarEmpresa(
    ILogger<BorrarEmpresa> logger,
    IEmpresaRepositorio empresaRepositorio
) : IBorrarEmpresa
{
    public async Task<ResultadoT<Guid>> BorrarEmpresaAsync(Guid id, CancellationToken cancellationToken)
    {
        var empresa = await empresaRepositorio.ObtenerByIdAsync(id, cancellationToken);
        if (empresa is null)
        {
            logger.LogWarning("No se encontró ninguna empresa con el ID {EmpresaId} para eliminar.", id);
            return ResultadoT<Guid>.Fallo(
                Error.NoEncontrado("404", $"No se encontró ninguna empresa con el ID {id}.")
            );
        }
        
        await empresaRepositorio.EliminarAsync(empresa, cancellationToken);
        
        logger.LogInformation("Empresa con ID {EmpresaId} eliminada correctamente.", empresa.EmpresaId);
        
        return ResultadoT<Guid>.Exito(empresa.EmpresaId);
    }
}