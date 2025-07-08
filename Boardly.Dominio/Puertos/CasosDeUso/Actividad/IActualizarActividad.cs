using Boardly.Dominio.Utilidades;

namespace Boardly.Dominio.Puertos.CasosDeUso.Actividad;

public interface IActualizarActividad<in TSolicitud, TRespuesta>
where TRespuesta : class
where TSolicitud : class
{
    Task<ResultadoT<TRespuesta>> ActualizarActividadAsync(Guid id, TSolicitud solicitud, CancellationToken cancellationToken);
}