using Boardly.Dominio.Utilidades;

namespace Boardly.Dominio.Puertos.CasosDeUso.Codigo;

public interface IObtenerCodigo<TDto> where TDto : class
{
    Task<ResultadoT<TDto>> ObtenerCodigoAsync(Guid codigoId, CancellationToken cancellationToken);
}