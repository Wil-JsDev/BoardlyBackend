using Boardly.Dominio.Modelos;
using Boardly.Dominio.Utilidades;

namespace Boardly.Dominio.Puertos.Repositorios;

public interface IProyectoRepositorio : IGenericoRepositorio<Proyecto>
{
    Task<ResultadoPaginado<Proyecto>> ObtenerPaginasProyectoAsync(Guid empresaId, int numeroPagina, int tamanoPagina, CancellationToken cancellationToken);
    
    Task<bool> ExisteProyectoAsync(string nombre, CancellationToken cancellationToken);
    
    Task<bool> NombreProyectoEnUsoAsync(Guid proyectoId, string nombre, CancellationToken cancellationToken);
    
}