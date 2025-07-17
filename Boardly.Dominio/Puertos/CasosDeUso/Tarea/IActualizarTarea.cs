using Boardly.Dominio.Utilidades;

namespace Boardly.Dominio.Puertos.CasosDeUso.Tarea;

public interface IActualizarTarea<in TSolicitud, TRespuesta>
    where TSolicitud : class
    where TRespuesta : class
{
    Task<ResultadoT<TRespuesta>> ActualizarTareaAsync(Guid tareaId, TSolicitud solicitud, CancellationToken cancellationToken);
}