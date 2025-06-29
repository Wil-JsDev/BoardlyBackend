using Boardly.Dominio.Utilidades;

namespace Boardly.Dominio.Puertos.CasosDeUso.Ceo;

public interface IResultadoPaginaCeo<in TSolicitud, TRespuesta>
{
    Task<ResultadoT<ResultadoPaginado<TRespuesta>>> ObtenerPaginacionCeoAsync(TSolicitud solicitud, CancellationToken cancellationToken);
}