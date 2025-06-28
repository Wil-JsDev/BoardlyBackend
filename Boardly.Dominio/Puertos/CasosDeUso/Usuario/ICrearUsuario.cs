using Boardly.Dominio.Utilidades;

namespace Boardly.Dominio.Puertos.CasosDeUso.Usuario;

public interface ICrearUsuario<in TSolicitud, TRespuesta> 
    where TSolicitud : class
    where TRespuesta : class
{
    Task<ResultadoT<TRespuesta>> CrearUsuarioAsync(TSolicitud solicitud, CancellationToken cancellationToken);
}