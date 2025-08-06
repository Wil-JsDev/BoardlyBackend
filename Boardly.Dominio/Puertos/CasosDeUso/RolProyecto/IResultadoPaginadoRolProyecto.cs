using Boardly.Dominio.Utilidades;

namespace Boardly.Dominio.Puertos.CasosDeUso.RolProyecto;

public interface IResultadoPaginadoRolProyecto<in TSolicitud, TRespuesta>
    where TSolicitud : class
    where TRespuesta : class
{
    Task<ResultadoT<ResultadoPaginado<TRespuesta>>> ObtenerPaginacionRolProyectoAsync(Guid proyectoId,TSolicitud solicitud, CancellationToken cancellationToken);
}