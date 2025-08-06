using Boardly.Dominio.Utilidades;

namespace Boardly.Dominio.Puertos.CasosDeUso.Ceo;

public interface IObtenerConteoDeEstadisticasCeo<TRespuesta>
{
    Task<ResultadoT<TRespuesta>> ObtenerConteoDeEstadisticasCeoAsync(Guid ceoId, CancellationToken cancellationToken);
}