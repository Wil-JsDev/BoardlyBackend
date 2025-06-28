using Boardly.Dominio.Utilidades;

namespace Boardly.Dominio.Puertos.CasosDeUso;

public interface ICodigoDisponible<TRespuesta> where TRespuesta : class
{
    Task<TRespuesta> CodigoDisponibleAsync(string codigo, CancellationToken cancellationToken);
}