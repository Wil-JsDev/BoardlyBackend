using Boardly.Dominio.Utilidades;

namespace Boardly.Dominio.Puertos.CasosDeUso.Usuario;

public interface IActualizarUsuario<in TSolicitud, TRespuesta>
    where TSolicitud : class
    where TRespuesta : class
{
    Task<ResultadoT<TRespuesta>> ActualizarUsuarioAsync(Guid id,TSolicitud solicitud, CancellationToken cancellationToken);
}