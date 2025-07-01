using Boardly.Dominio.Utilidades;

namespace Boardly.Dominio.Puertos.CasosDeUso.Empleado;

public interface IObtenerIdEmpleado<TRespuesta>
    where TRespuesta : class
{
    Task<ResultadoT<TRespuesta>> ObtenerIdEmpleadoAsync(Guid id, CancellationToken cancellationToken);
}