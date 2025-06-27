using Boardly.Dominio.Utilidades;

namespace Boardly.Dominio.Puertos.CasosDeUso;

public interface IConfirmarCuenta<TRespuesta> where TRespuesta : class
{
    Task<TRespuesta> ConfirmarCuentaAsync(Guid usuarioId, string codigo, CancellationToken cancellationToken);
}