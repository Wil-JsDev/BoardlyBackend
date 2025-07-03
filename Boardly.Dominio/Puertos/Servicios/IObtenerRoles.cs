namespace Boardly.Dominio.Puertos.Servicios;

public interface IObtenerRoles
{
    Task<IList<string>> ObtenerRolesAsync(Guid usuarioId, CancellationToken cancellationToken);
}