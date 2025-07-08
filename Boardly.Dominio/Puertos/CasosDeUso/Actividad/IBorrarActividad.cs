using Boardly.Dominio.Utilidades;

namespace Boardly.Dominio.Puertos.CasosDeUso.Actividad;

public interface IBorrarActividad
{
    Task<ResultadoT<Guid>> BorrarActividadAsync(Guid id, CancellationToken cancellationToken);
}