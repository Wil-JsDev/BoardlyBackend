using Boardly.Dominio.Utilidades;

namespace Boardly.Dominio.Puertos.CasosDeUso.Actividad;

public interface IObtenerIdActividad<TRespuesta>
where TRespuesta : class
{
    Task<ResultadoT<TRespuesta>> ObtenerIdActividadAsync(Guid id, CancellationToken cancellationToken);
}