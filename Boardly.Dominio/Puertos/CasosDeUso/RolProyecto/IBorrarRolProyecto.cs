using Boardly.Dominio.Utilidades;

namespace Boardly.Dominio.Puertos.CasosDeUso.RolProyecto;

public interface IBorrarRolProyecto
{
    Task<ResultadoT<Guid>> BorrarRolProyectoAsync(Guid id, CancellationToken cancellationToken);
}