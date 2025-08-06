using Boardly.Dominio.Utilidades;

namespace Boardly.Dominio.Puertos.CasosDeUso.Usuario;

public interface IActualizarNombreUsuario<in TSolicitud ,TRespuesta>
    where TSolicitud : class
    where TRespuesta : class
{
    Task<ResultadoT<TRespuesta>> ActualizarNombreUsuarioAsync(Guid id, TSolicitud solicitud, CancellationToken cancellationToken);
}