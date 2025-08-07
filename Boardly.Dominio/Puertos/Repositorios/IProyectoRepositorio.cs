using Boardly.Dominio.Modelos;
using Boardly.Dominio.Utilidades;

namespace Boardly.Dominio.Puertos.Repositorios;

public interface IProyectoRepositorio : IGenericoRepositorio<Proyecto>
{
    Task<ResultadoPaginado<Proyecto>> ObtenerPaginasProyectoAsync(Guid empresaId, int numeroPagina, int tamanoPagina, CancellationToken cancellationToken);

    Task<Proyecto> ObtenerProyectoEmpleadosPorIdAsync(Guid proyectoId, CancellationToken cancellationToken);

    
    Task<bool> ExisteProyectoAsync(string nombre, CancellationToken cancellationToken);
    
    Task<bool> NombreProyectoEnUsoAsync(Guid proyectoId, string nombre, CancellationToken cancellationToken);
    
    Task<int> ObtenerConteoDeTareasCompletadasProyectosAsync(Guid proyectoId,
        CancellationToken cancellationToken);
    
    Task<int> ObtenerConteoDeTareasPendienteProyectosAsync(Guid proyectoId,
        CancellationToken cancellationToken);

    Task<int> ObtenerConteoDeTareasProyectosAsync(Guid proyectoId, CancellationToken cancellationToken);

    Task<int> ObtenerConteoDeActividadesProyectosAsync(Guid proyectoId, CancellationToken cancellationToken);

    Task<ResultadoPaginado<Proyecto>> ProyectosFinalizados(Guid empresaId,
        int numeroPagina,
        int tamanoPagina,
        CancellationToken cancellationToken);
}