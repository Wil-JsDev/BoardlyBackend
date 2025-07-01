using Boardly.Dominio.Utilidades;

namespace Boardly.Dominio.Puertos.CasosDeUso.Empleado;

public interface ICrearEmpleado<in TSolicitud, TRespuesta>
    where TSolicitud : class
    where TRespuesta : class
{
    Task<ResultadoT<TRespuesta>> CrearEmpleadoAsync(TSolicitud solicitud, CancellationToken cancellationToken);
}