using Boardly.Dominio.Utilidades;

namespace Boardly.Dominio.Puertos.CasosDeUso.Usuario;

public interface IRestablecerContrasena<in TSolicitud>
where TSolicitud : class
{
    Task<ResultadoT<string>> RestablecerContrasenaAsync(TSolicitud solicitud, CancellationToken cancellationToken);
}