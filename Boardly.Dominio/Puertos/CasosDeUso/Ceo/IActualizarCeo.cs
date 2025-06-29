using Boardly.Dominio.Utilidades;

namespace Boardly.Dominio.Puertos.CasosDeUso.Ceo;

public interface IActualizarCeo<in TSolicitud, TRespuesta>
    where TSolicitud : class
    where TRespuesta : class
{
    Task<ResultadoT<TRespuesta>> ActualizarCeoAsync(TSolicitud solicitud, CancellationToken cancellationToken);
}