using Boardly.Dominio.Utilidades;

namespace Boardly.Dominio.Puertos.CasosDeUso.Empresa;

public interface IBorrarEmpresa
{
    Task<ResultadoT<Guid>>  BorrarEmpresaAsync(Guid id, CancellationToken cancellationToken);
}