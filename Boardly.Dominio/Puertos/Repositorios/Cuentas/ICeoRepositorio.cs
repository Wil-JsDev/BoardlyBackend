using Boardly.Dominio.Modelos;

namespace Boardly.Dominio.Puertos.Repositorios.Cuentas;

public interface ICeoRepositorio : IGenericoRepositorio<Ceo>
{
    Task<Guid> ObtenerIdCeoAsync(Guid usuarioId, CancellationToken cancellationToken);
}