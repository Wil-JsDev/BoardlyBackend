using Boardly.Dominio.Utilidades;

namespace Boardly.Dominio.Puertos.CasosDeUso.Proyecto;

public interface IBorrarProyecto
{
    Task<ResultadoT<Guid>> BorrarProyectoAsync(Guid id, CancellationToken cancellationToken);
}