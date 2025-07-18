using Boardly.Dominio.Utilidades;

namespace Boardly.Dominio.Puertos.CasosDeUso.Tarea;

public interface IResultadoPaginadoTarea<in TSolicitud, TRespuesta>
    where TSolicitud : class
    where TRespuesta : class
{
    Task<ResultadoT<ResultadoPaginado<TRespuesta>>> ObtenerPaginacionTareaAsync(Guid actividadId, TSolicitud solicitud, CancellationToken cancellationToken);
}