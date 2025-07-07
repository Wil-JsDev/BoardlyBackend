using Boardly.Dominio.Utilidades;

namespace Boardly.Dominio.Puertos.CasosDeUso.Proyecto;

public interface IActualizarProyecto<in TSolicitud, TRespuesta>
where TSolicitud : class
where TRespuesta : class
{
    Task<ResultadoT<TRespuesta>> ActualizarProyectoAsync(Guid id, TSolicitud solicitud, CancellationToken cancellationToken);
}