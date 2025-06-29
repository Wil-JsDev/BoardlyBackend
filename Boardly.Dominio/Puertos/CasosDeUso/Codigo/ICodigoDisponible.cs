namespace Boardly.Dominio.Puertos.CasosDeUso.Codigo;

public interface ICodigoDisponible<TRespuesta> where TRespuesta : class
{
    Task<TRespuesta> CodigoDisponibleAsync(string codigo, CancellationToken cancellationToken);
}