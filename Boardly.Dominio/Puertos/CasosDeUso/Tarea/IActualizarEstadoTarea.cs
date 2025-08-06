using Boardly.Dominio.Enum;
using Boardly.Dominio.Utilidades;

namespace Boardly.Dominio.Puertos.CasosDeUso.Tarea;

public interface IActualizarEstadoTarea
{
    Task<Resultado> CambiarEstadoAsync(Guid tareaId, EstadoTarea nuevoEstado, bool enRevision ,CancellationToken cancellationToken);
}