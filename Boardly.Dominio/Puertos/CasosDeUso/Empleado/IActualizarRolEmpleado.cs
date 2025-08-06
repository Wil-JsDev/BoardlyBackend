using Boardly.Dominio.Utilidades;

namespace Boardly.Dominio.Puertos.CasosDeUso.Empleado;

public interface IActualizarRolEmpleado<in TSolicitud , TRespuesta>
    where TRespuesta : class
    where TSolicitud : class
{
    Task<ResultadoT<TRespuesta>> ActualizarRolEmpleadoAsync(Guid id, TSolicitud solicitud, CancellationToken cancellationToken);
}