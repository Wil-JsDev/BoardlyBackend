using Boardly.Dominio.Utilidades;

namespace Boardly.Dominio.Puertos.CasosDeUso.Empleado;

public interface IResultadoPaginadoEmpleado<in TSolicitud, TRespuesta>
    where TRespuesta : class
    where TSolicitud : class
{
    Task<ResultadoT<ResultadoT<ResultadoPaginado<TRespuesta>>>> ObtenerPaginacionEmpleadoAsync(TSolicitud solicitud, CancellationToken cancellationToken);
}