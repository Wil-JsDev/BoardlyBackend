using Boardly.Dominio.Modelos;

namespace Boardly.Dominio.Puertos.Repositorios;

public interface IActividadRepositorio : IGenericoRepositorio<Actividad>
{
    Task<bool> ExisteNombreActividadAsync(string nombreEmpresa, CancellationToken cancellationToken);
    
    Task<bool> NombreActividadEnUso(string nombreUsuario, Guid actividadId, CancellationToken cancellationToken);
}