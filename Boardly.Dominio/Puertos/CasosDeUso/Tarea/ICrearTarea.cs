using Boardly.Dominio.Utilidades;

namespace Boardly.Dominio.Puertos.CasosDeUso.Tarea;

public interface ICrearTarea<in TSolicitud, TRespuesta>
    where TSolicitud : class
    where TRespuesta : class
{
    Task<ResultadoT<TRespuesta>> CrearTareaAsync(TSolicitud solicitud, CancellationToken cancellationToken);
}