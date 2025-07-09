using Boardly.Dominio.Modelos;

namespace Boardly.Dominio.Puertos.Repositorios.Cuentas;

public interface IEmpleadoRepositorio :  IGenericoRepositorio<Empleado>
{
    Task<IEnumerable<Empleado>> ObtenerPorEmpresaIdAsync(Guid empresaId, CancellationToken cancellationToken);
}