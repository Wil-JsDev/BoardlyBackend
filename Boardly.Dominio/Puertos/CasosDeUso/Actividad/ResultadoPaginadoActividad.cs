using Boardly.Dominio.Utilidades;

namespace Boardly.Dominio.Puertos.CasosDeUso.Actividad;

public interface IResultadoPaginadoActividad<in TSolicitud, TRespuesta>
where TRespuesta : class
where TSolicitud : class
{
    Task<ResultadoT<ResultadoPaginado<TRespuesta>>> ObtenerPaginacionActividadAsync(Guid proyectoId, TSolicitud solicitud, CancellationToken cancellationToken);
}