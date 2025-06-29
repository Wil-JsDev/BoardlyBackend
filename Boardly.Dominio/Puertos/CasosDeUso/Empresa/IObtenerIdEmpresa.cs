using Boardly.Dominio.Utilidades;

namespace Boardly.Dominio.Puertos.CasosDeUso.Empresa;

public interface IObtenerIdEmpresa<TRespuesta>
{
    Task<ResultadoT<TRespuesta>> ObtenerEmpresaIdAsync(Guid id, CancellationToken cancellationToken);
}