using Boardly.Dominio.Utilidades;

namespace Boardly.Dominio.Puertos.CasosDeUso.Usuario;

public interface IModificarContrasenaUsuario<in TSolicitud>
    where TSolicitud : class
{
    Task<ResultadoT<string>>  ModificarContasenaAsync(TSolicitud solicitud, CancellationToken cancellationToken);
}