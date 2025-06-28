using Boardly.Dominio.Utilidades;

namespace Boardly.Dominio.Puertos.CasosDeUso.Usuario;

public interface IOlvidarContrasenaUsuario
{
    Task<ResultadoT<string>> OlvidarContrasena(Guid usuarioId, CancellationToken cancellationToken);
}