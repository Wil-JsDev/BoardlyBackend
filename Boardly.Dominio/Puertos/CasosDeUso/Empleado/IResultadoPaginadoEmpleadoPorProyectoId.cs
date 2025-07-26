using Boardly.Dominio.Utilidades;

namespace Boardly.Dominio.Puertos.CasosDeUso.Empleado;

public interface IResultadoPaginadoEmpleadoPorProyectoId<in TSolicitud, TRespuesta>
    where TSolicitud : class
    where TRespuesta : class
{
    Task<ResultadoT<ResultadoPaginado<TRespuesta>>> ObtenerPaginacionEmpleadoPorProyectoIdAsync(Guid proyectoId, TSolicitud solicitud, CancellationToken cancellationToken);
}