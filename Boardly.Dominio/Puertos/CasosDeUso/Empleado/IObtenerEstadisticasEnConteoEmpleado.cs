using Boardly.Dominio.Utilidades;

namespace Boardly.Dominio.Puertos.CasosDeUso.Empleado;

public interface IObtenerEstadisticasEnConteoEmpleado<TRespuesta>
{
    Task<ResultadoT<TRespuesta>> ObtenerEstadisticasEnConteoEmpleadoAsync(Guid empleadoId,CancellationToken cancellationToken);
}