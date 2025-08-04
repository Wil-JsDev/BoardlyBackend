using Boardly.Dominio.Utilidades;

namespace Boardly.Dominio.Puertos.CasosDeUso.Empleado;

public interface IBorrarEmpleado
{
    Task<ResultadoT<Guid>> BorrarEmpleadoAsync(Guid rolProyectoId, CancellationToken cancellationToken);
}