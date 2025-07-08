namespace Boardly.Dominio.Puertos.Servicios;

public interface IObtenerEmpleadoId
{
    Task<Guid?> ObtenerEmpleadoIdAsync(Guid usuarioId, CancellationToken cancellationToken);
}