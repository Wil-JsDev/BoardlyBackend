using Boardly.Dominio.Modelos;
using Boardly.Dominio.Utilidades;

namespace Boardly.Dominio.Puertos.Repositorios;

public interface IActividadRepositorio : IGenericoRepositorio<Actividad>
{
    Task<ResultadoPaginado<Actividad>> ObtenerPaginasActividadByIdProyectoAsync(Guid proyectoId, int numeroPagina, int tamanoPagina, CancellationToken cancellationToken);

    Task<bool> ExisteNombreActividadAsync(string nombreEmpresa, CancellationToken cancellationToken);
    
    Task<bool> NombreActividadEnUso(string nombreUsuario, Guid actividadId, CancellationToken cancellationToken);
}