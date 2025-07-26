using Boardly.Dominio.Modelos;
using Boardly.Dominio.Utilidades;

namespace Boardly.Dominio.Puertos.Repositorios.Cuentas;

public interface IEmpleadoRepositorio :  IGenericoRepositorio<Empleado>
{
    Task<IEnumerable<Empleado>> ObtenerPorEmpresaIdAsync(Guid empresaId, CancellationToken cancellationToken);
    Task<int> ObtenerConteoDeEmpresasQuePerteneceAsync(Guid empleadoId, CancellationToken cancellationToken);
    Task<int> ObtenerConteoDeProyectosQuePerteneceAsync(Guid empleadoId, CancellationToken cancellationToken);
    Task<int> ObtenerConteoDeActividadesQuePerteneceAsync(Guid empleadoId,
        CancellationToken cancellationToken);
    Task<int> ObtenerConteoDeTareasQuePerteneceAsync(Guid empleadoId, CancellationToken cancellationToken);
    Task<int> ObtenerConteoDeTareasEnProcesoQuePertenceAsync(Guid empleadoId, CancellationToken cancellationToken);
    Task<int> ObtenerConteoDeTareasAVencerAsync(Guid empleadoId, CancellationToken cancellationToken);

    Task<ResultadoPaginado<Empleado>> ObtenerPaginasEmpleadoProyectoIdAsync(Guid proyectoId,
        int numeroPagina,
        int tamanoPagina,
        CancellationToken cancellationToken);
}