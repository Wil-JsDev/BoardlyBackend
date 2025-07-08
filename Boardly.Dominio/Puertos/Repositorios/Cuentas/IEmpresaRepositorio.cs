using Boardly.Dominio.Modelos;
using Boardly.Dominio.Utilidades;

namespace Boardly.Dominio.Puertos.Repositorios.Cuentas;

public interface IEmpresaRepositorio: IGenericoRepositorio<Empresa>
{
    Task<bool> ExisteNombreEmpresaAsync(string nombreEmpresa, CancellationToken cancellationToken);
    Task<bool> NombreEmpresaEnUso(string nombreEmpresa, Guid empresaId, CancellationToken cancellationToken);
    Task<ResultadoPaginado<Empresa>> ObtenerPaginasEmpresaAsync(Guid ceoId, int numeroPagina, int tamanoPagina,
        CancellationToken cancellationToken);
}