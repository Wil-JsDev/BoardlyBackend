using Boardly.Dominio.Utilidades;

namespace Boardly.Dominio.Puertos.CasosDeUso.Empleado;

public interface IObtenerEmpleadoPorEmpresaId<TRespuesta>
{
    Task<ResultadoT<IEnumerable<TRespuesta>>> ObtenerEmpleadoPorEmpresaIdAsync(Guid empresaId, CancellationToken cancellationToken);
}