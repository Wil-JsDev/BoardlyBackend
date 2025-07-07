using Boardly.Dominio.Utilidades;

namespace Boardly.Dominio.Puertos.CasosDeUso.Actividad;

public interface ICrearActividad<in TSolicitud, TRespuesta>
where TRespuesta : class
where TSolicitud : class
{
    Task<ResultadoT<TRespuesta>> CrearActividadAsync(TSolicitud solicitud, CancellationToken cancellationToken);
}