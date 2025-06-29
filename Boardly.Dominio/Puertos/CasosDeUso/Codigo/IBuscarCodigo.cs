using Boardly.Dominio.Utilidades;

namespace Boardly.Dominio.Puertos.CasosDeUso.Codigo;

public interface IBuscarCodigo<TRespuesta>
 where TRespuesta : class
{
    Task<ResultadoT<TRespuesta>> BuscarCodigoAsync(string codigo, CancellationToken cancellationToken);
}