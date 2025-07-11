using Boardly.Dominio.Utilidades;

namespace Boardly.Dominio.Puertos.CasosDeUso.RolProyecto;

public interface ICrearRolProyecto<in TSolicitud, TRespuesta>
where TSolicitud : class
where TRespuesta : class
{
    Task<ResultadoT<TRespuesta>> CrearRolProyectoAsync(TSolicitud solicitud, CancellationToken cancellationToken);
}