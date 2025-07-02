using Boardly.Dominio.Utilidades;

namespace Boardly.Dominio.Puertos.CasosDeUso.Autenticacion;

public interface IAutenticacion<TRespuesta, TSolicitud>
    where TRespuesta: class 
    where TSolicitud: class
{
    Task<ResultadoT<TRespuesta>> AutenticarAsync(TSolicitud solicitud, CancellationToken cancellationToken);
}