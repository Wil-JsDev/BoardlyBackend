using Boardly.Dominio.Utilidades;

namespace Boardly.Dominio.Puertos.CasosDeUso;

public interface IEliminarCodigo<TRespuesta>  where TRespuesta : class
{
    Task<TRespuesta> EliminarCodigoAsync(Guid codigoId, CancellationToken cancellationToken);

}