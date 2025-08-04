using Boardly.Dominio.Utilidades;

namespace Boardly.Dominio.Puertos.CasosDeUso.Proyecto;

public interface IObtenerProyectoRoles<TRespuesta>
    where TRespuesta : class
{
    Task<ResultadoT<TRespuesta>> ObtenerProyectoRolesAsync(Guid id, CancellationToken cancellationToken);

}