using Boardly.Dominio.Utilidades;

namespace Boardly.Dominio.Puertos.CasosDeUso.Empresa;

public interface IResultadoPaginaPorEmpleadoIdEmpresa<in TSolicitud, TRespuesta>
    where TSolicitud : class
    where TRespuesta : class
{
    Task<ResultadoT<ResultadoPaginado<TRespuesta>>> ObtenerPaginacionPorEmpleadoIdEmpresaAsync(Guid empleadoId, TSolicitud solicitud, CancellationToken cancellationToken);   
}