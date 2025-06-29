using Boardly.Dominio.Utilidades;

namespace Boardly.Dominio.Puertos.CasosDeUso.Usuario;

public interface IResultadoPaginaUsuario<in TSolicitud, TRespuesta>
    where TSolicitud : class
    where TRespuesta : class
{
    Task<ResultadoT<ResultadoPaginado<TRespuesta>>>  ObtenerPaginacionUsuarioAsync(TSolicitud solicitud, CancellationToken cancellationToken);
}