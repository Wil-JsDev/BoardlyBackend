using Boardly.Dominio.Utilidades;

namespace Boardly.Dominio.Puertos.CasosDeUso.Empleado;

public interface IAgregarEmpleadoProyecto<in TSolicitud, TRespuesta>
    where TSolicitud : class
    where TRespuesta : class
{
    Task<ResultadoT<TRespuesta>> AgregarEmpleadoProyectoAsync(Guid empleadoId, TSolicitud solicitud, CancellationToken cancellationToken);
}