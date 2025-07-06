using Boardly.Dominio.Utilidades;

namespace Boardly.Dominio.Puertos.CasosDeUso.Ceo;

public interface IObtenerIdCeo
{
    Task<ResultadoT<Guid>> ObtenerIdCeoAsync(Guid id, CancellationToken cancellationToken);
}