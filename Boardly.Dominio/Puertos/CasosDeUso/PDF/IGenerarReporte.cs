using Boardly.Dominio.Utilidades;

namespace Boardly.Dominio.Puertos.CasosDeUso.PDF;

public interface IGenerarReporte<TSolicitud>
{
    Task<ResultadoT<byte[]>> GenerarReporteAsync(TSolicitud solicitud, CancellationToken cancellationToken);
}