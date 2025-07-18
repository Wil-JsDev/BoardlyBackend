using Boardly.Dominio.Utilidades;

namespace Boardly.Dominio.Puertos.CasosDeUso.Tarea;

public interface IObtenerIdTarea<TRespuesta>
    where TRespuesta : class
{
    Task<ResultadoT<TRespuesta>> ObtenerIdUsuarioAsync(Guid tareaId, CancellationToken cancellationToken);
}