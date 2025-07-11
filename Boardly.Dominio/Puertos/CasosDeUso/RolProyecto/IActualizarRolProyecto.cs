using Boardly.Dominio.Utilidades;

namespace Boardly.Dominio.Puertos.CasosDeUso.RolProyecto;

public interface IActualizarRolProyecto<in TSolicitud, TRespuesta>
    where TSolicitud : class
    where TRespuesta : class
{
    Task<ResultadoT<TRespuesta>> ActualizarRolProyectoAsync(Guid id, TSolicitud solicitud, CancellationToken cancellationToken);
}