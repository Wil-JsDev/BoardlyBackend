using Boardly.Dominio.Modelos;
using Boardly.Dominio.Utilidades;

namespace Boardly.Dominio.Puertos.Repositorios;

public interface IRolProyectoRepositorio : IGenericoRepositorio<RolProyecto>
{
    Task<bool> ExisteNombreRolProyectoAsync(Guid proyectoId, string nombreEmpresa, CancellationToken cancellationToken);

    Task<bool> ExisteNombreRolProyectoEnActualizacionAsync(Guid rolProyectoId, Guid proyectoId, string nombreRol,
        CancellationToken cancellationToken);
    
    Task<ResultadoPaginado<RolProyecto>> ObtenerPaginasRolProyectoAsync(Guid proyectoId, int numeroPagina, int tamanoPagina,
        CancellationToken cancellationToken);

    Task<Guid> ObtenerIdPorNombreAsync(string nombre, CancellationToken cancellationToken);
}