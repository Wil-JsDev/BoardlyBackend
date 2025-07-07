using Boardly.Dominio.Modelos;

namespace Boardly.Dominio.Puertos.Repositorios.Cuentas;

public interface IEmpresaRepositorio: IGenericoRepositorio<Empresa>
{
    Task<bool> ExisteNombreEmpresaAsync(string nombreEmpresa, CancellationToken cancellationToken);
    Task<bool> NombreEmpresaEnUso(string nombreEmpresa, Guid empresaId, CancellationToken cancellationToken);

}