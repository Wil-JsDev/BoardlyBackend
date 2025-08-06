using Boardly.Dominio.Utilidades;

namespace Boardly.Dominio.Puertos.CasosDeUso.Proyecto;

public interface IObtenerIdProyecto<TRespuesta>
    where TRespuesta : class
{
    Task<ResultadoT<TRespuesta>> ObtenerIdProyectoAsync(Guid id, CancellationToken cancellationToken);
}