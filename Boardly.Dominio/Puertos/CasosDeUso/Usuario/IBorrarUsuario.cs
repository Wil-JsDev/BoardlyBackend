using Boardly.Dominio.Utilidades;

namespace Boardly.Dominio.Puertos.CasosDeUso.Usuario;

public interface IBorrarUsuario
{
    Task<ResultadoT<Guid>>  BorrarUsuarioAsync(Guid id, CancellationToken cancellationToken);
}