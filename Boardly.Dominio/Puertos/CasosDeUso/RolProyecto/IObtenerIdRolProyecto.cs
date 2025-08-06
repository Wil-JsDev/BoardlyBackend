using Boardly.Dominio.Utilidades;

namespace Boardly.Dominio.Puertos.CasosDeUso.RolProyecto;

public interface IObtenerIdRolProyecto<TRespuesta>
where TRespuesta : class
{
    Task<ResultadoT<TRespuesta>> ObtenerIdRolProyectoAsync(Guid id, CancellationToken cancellationToken);
}