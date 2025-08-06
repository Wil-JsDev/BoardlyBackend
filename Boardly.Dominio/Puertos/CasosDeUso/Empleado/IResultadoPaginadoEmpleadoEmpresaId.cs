using Boardly.Dominio.Utilidades;

namespace Boardly.Dominio.Puertos.CasosDeUso.Empleado;

public interface IResultadoPaginadoEmpleadoEmpresaId<in TSolicitud, TRespuesta>
where TSolicitud : class
where TRespuesta : class
{
    Task<ResultadoT<ResultadoPaginado<TRespuesta>>> ResultadoPaginadoEmpleadoEmpresaIdAsync(Guid empresaId, TSolicitud solicitud , CancellationToken cancellationToken);
}