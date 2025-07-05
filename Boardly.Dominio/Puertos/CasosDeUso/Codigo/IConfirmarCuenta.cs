namespace Boardly.Dominio.Puertos.CasosDeUso.Codigo;

public interface IConfirmarCuenta<TRespuesta, TSolicitud> where TRespuesta : class
{
    Task<TRespuesta> ConfirmarCuentaAsync(Guid usuarioId, TSolicitud solicitud, CancellationToken cancellationToken);
}