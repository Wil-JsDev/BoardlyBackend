using Boardly.Dominio.Utilidades;

namespace Boardly.Dominio.Puertos.CasosDeUso.Ceo;

public interface IBorrarCeo
{
    Task<ResultadoT<Guid>> BorrarCeoAsync(Guid id, CancellationToken cancellationToken);
}