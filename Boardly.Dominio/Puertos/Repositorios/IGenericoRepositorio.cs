using System.Linq.Expressions;
using Boardly.Dominio.Utilidades;

namespace Boardly.Dominio.Puertos.Repositorios;

public interface IGenericoRepositorio<TEntidad>
{
    Task<TEntidad> ObtenerByIdAsync(Guid id, CancellationToken cancellationToken);
    
    Task<ResultadoPaginado<TEntidad>> ObtenerPaginadoAsync(int numeroPagina, int tamanioPagina ,CancellationToken cancellationToken);
    
    Task CrearAsync(TEntidad entidad, CancellationToken cancellationToken);
    
    Task ActualizarAsync(TEntidad entidad, CancellationToken cancellationToken);
    
    Task EliminarAsync(TEntidad entidad, CancellationToken cancellationToken);
    
    Task<bool> ValidarAsync(Expression<Func<TEntidad, bool>> predicate, CancellationToken cancellationToken);
    
    Task GuardarAsync(CancellationToken cancellationToken);
}