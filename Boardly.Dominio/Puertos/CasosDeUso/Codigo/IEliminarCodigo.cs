namespace Boardly.Dominio.Puertos.CasosDeUso.Codigo;

public interface IEliminarCodigo<TRespuesta>  where TRespuesta : class
{
    Task<TRespuesta> EliminarCodigoAsync(Guid codigoId, CancellationToken cancellationToken);

}