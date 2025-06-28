using Boardly.Dominio.Utilidades;

namespace Boardly.Dominio.Puertos.CasosDeUso.Usuario;

public interface IObtenerIdUsuario<TRespuesta>
    where TRespuesta : class
{
    Task<ResultadoT<TRespuesta>> ObtenerIdUsarioAsync(Guid id, CancellationToken cancellationToken);
}