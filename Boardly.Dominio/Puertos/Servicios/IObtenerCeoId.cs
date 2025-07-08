namespace Boardly.Dominio.Puertos.Servicios;

public interface IObtenerCeoId
{
    Task<Guid?> ObtenerCeoIdAsync(Guid usuarioId,CancellationToken cancellationToken);
}