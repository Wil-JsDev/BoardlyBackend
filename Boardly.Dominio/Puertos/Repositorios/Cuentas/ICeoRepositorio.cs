using Boardly.Dominio.Modelos;

namespace Boardly.Dominio.Puertos.Repositorios.Cuentas;

public interface ICeoRepositorio : IGenericoRepositorio<Ceo>
{
    Task<Guid> ObtenerIdCeoAsync(Guid usuarioId, CancellationToken cancellationToken);

    Task<int> ObtenerConteoDeEmpleadosDentroDeLaEmpresaAsync(Guid ceoId,
        CancellationToken cancellationToken);

    Task<int> ObtenerConteoDeEmpleadosActivosDeLaEmpresaAsync(Guid ceoId,
        CancellationToken cancellationToken);

    Task<int> ObtenerConteoDeEmpleadosInactivosDeLaEmpresaAsync(Guid ceoId,
        CancellationToken cancellationToken);

    Task<int> ObtenerConteoDeEmpresasCeoAsync(Guid ceoId, CancellationToken cancellationToken);

    Task<int> ObtenerConteoDeProyectosAsync(Guid ceoId, CancellationToken cancellationToken);
}