using Boardly.Dominio.Utilidades;

namespace Boardly.Dominio.Puertos.CasosDeUso.Ceo;

public interface IObtenerIdCeo<TRespuesta>
{
    Task<ResultadoT<TRespuesta>> ObtenerIdCeoAsync(Guid id, CancellationToken cancellationToken);
}