using Boardly.Dominio.Utilidades;

namespace Boardly.Dominio.Puertos.CasosDeUso.Proyecto;

public interface IResultadoPaginaProyectoEmpleado<in TSolicitud, TRespuesta>
where TSolicitud : class
where TRespuesta : class
{
    Task<ResultadoT<ResultadoPaginado<TRespuesta>>> ObtenerPaginacionProyectoEmpleadoAsync(Guid empresaId, Guid empleadoId,
        TSolicitud solicitud, CancellationToken cancellationToken);
}