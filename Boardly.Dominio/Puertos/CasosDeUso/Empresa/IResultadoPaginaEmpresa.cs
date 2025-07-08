using Boardly.Dominio.Utilidades;

namespace Boardly.Dominio.Puertos.CasosDeUso.Empresa;

public interface IResultadoPaginaEmpresa<in TSolicitud, TRespuesta>
    where TSolicitud : class
    where TRespuesta : class
{
    Task<ResultadoT<ResultadoPaginado<TRespuesta>>> ObtenerPaginacionEmpresaAsync(Guid ceoId,TSolicitud solicitud, CancellationToken cancellationToken);
}