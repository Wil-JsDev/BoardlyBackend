using Boardly.Dominio.Utilidades;

namespace Boardly.Dominio.Puertos.CasosDeUso.Usuario;

public interface IActualizarApellidoUsuario<in TSolicitud, TRespuesta>
    where TSolicitud : class
    where TRespuesta : class
{ 
    Task<ResultadoT<TRespuesta>> ActualizarApellidoUsuarioAsync(Guid id, TSolicitud solicitud, CancellationToken cancellationToken);
    
}