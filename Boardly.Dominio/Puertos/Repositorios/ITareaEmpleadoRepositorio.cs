using Boardly.Dominio.Modelos;

namespace Boardly.Dominio.Puertos.Repositorios;

public interface ITareaEmpleadoRepositorio : IGenericoRepositorio<TareaEmpleado>
{
    Task CrearTareasEmpleadosAsync(List<TareaEmpleado> tareasEmpleados, CancellationToken cancellationToken);

    Task<TareaEmpleado?> ObtenerEmpleadoIdsAsync(IEnumerable<Guid> empleadosIds, CancellationToken cancellationToken);

    Task ActualizarTareasEmpleadosAsync(Guid tareaId, List<Guid> nuevosEmpleadoIds,
        CancellationToken cancellationToken);
}