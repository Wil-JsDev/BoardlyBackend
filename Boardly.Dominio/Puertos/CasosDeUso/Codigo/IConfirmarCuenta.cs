namespace Boardly.Dominio.Puertos.CasosDeUso.Codigo;

public interface IConfirmarCuenta<TRespuesta> where TRespuesta : class
{
    Task<TRespuesta> ConfirmarCuentaAsync(Guid usuarioId, string codigo, CancellationToken cancellationToken);
}