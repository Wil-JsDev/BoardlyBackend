using Boardly.Dominio.Utilidades;

namespace Boardly.Dominio.Puertos.CasosDeUso.Empresa;

public interface IActualizarEmpresa<in TSolicitud, TRespuesta>
    where TSolicitud : class
    where TRespuesta : class
{
    Task<ResultadoT<TRespuesta>> ActualizarEmpresaAsync(Guid id, TSolicitud solicitud, CancellationToken cancellationToken);
}