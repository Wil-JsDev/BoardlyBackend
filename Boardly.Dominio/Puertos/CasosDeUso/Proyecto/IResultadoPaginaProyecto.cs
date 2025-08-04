using Boardly.Dominio.Utilidades;

namespace Boardly.Dominio.Puertos.CasosDeUso.Proyecto;

public interface IResultadoPaginaProyecto<in TSolicitud, TRespuesta>
    where TSolicitud : class
    where TRespuesta : class
{
    Task<ResultadoT<ResultadoPaginado<TRespuesta>>> ObtenerPaginacionProyectoAsync(Guid empresaId,
        TSolicitud solicitud, CancellationToken cancellationToken);
}