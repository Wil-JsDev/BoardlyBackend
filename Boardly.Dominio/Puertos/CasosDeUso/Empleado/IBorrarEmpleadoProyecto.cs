using Boardly.Dominio.Utilidades;

namespace Boardly.Dominio.Puertos.CasosDeUso.Empleado;

public interface IBorrarEmpleadoProyecto
{
    Task<ResultadoT<string>> BorrarEmpleadoProyectoAsync(Guid empleadoId, Guid proyectoId, CancellationToken cancellationToken);
}