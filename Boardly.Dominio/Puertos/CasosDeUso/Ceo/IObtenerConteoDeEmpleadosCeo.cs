using Boardly.Dominio.Utilidades;

namespace Boardly.Dominio.Puertos.CasosDeUso.Ceo;

public interface IObtenerConteoDeEmpleadosCeo<TRespuesta>
{
    Task<ResultadoT<TRespuesta>> ObtenerConteoDeEmpleadosCeoAsync(Guid ceoId,CancellationToken cancellationToken);
}