using Boardly.Dominio.Modelos;
using Boardly.Dominio.Utilidades;

namespace Boardly.Dominio.Puertos.Repositorios.Cuentas;

/// <summary>
/// Repositorio específico para operaciones relacionadas con la entidad <see cref="Empresa"/>.
/// </summary>
public interface IEmpresaRepositorio : IGenericoRepositorio<Empresa>
{
    /// <summary>
    /// Verifica si ya existe una empresa con el nombre especificado.
    /// </summary>
    /// <param name="nombreEmpresa">Nombre de la empresa a verificar.</param>
    /// <param name="cancellationToken">Token para cancelar la operación asincrónica.</param>
    /// <returns>True si existe, de lo contrario false.</returns>
    Task<bool> ExisteNombreEmpresaAsync(string nombreEmpresa, CancellationToken cancellationToken);

    /// <summary>
    /// Verifica si el nombre de la empresa está en uso por otra empresa diferente a la proporcionada.
    /// </summary>
    /// <param name="nombreEmpresa">Nombre a verificar.</param>
    /// <param name="empresaId">ID de la empresa actual (para excluirla).</param>
    /// <param name="cancellationToken">Token para cancelar la operación asincrónica.</param>
    /// <returns>True si el nombre está en uso por otra empresa, de lo contrario false.</returns>
    Task<bool> NombreEmpresaEnUso(string nombreEmpresa, Guid empresaId, CancellationToken cancellationToken);

    /// <summary>
    /// Obtiene una lista paginada de empresas asociadas a un CEO.
    /// </summary>
    /// <param name="ceoId">ID del CEO.</param>
    /// <param name="numeroPagina">Número de página.</param>
    /// <param name="tamanoPagina">Tamaño de página.</param>
    /// <param name="cancellationToken">Token para cancelar la operación asincrónica.</param>
    /// <returns>Resultado paginado de empresas.</returns>
    Task<ResultadoPaginado<Empresa>> ObtenerPaginasEmpresaAsync(Guid ceoId, int numeroPagina, int tamanoPagina,
        CancellationToken cancellationToken);

    /// <summary>
    /// Obtiene todos los detalles de las empresas asociadas a un empleado.
    /// </summary>
    /// <param name="empleadoId">ID del empleado.</param>
    /// <param name="cancellationToken">Token para cancelar la operación asincrónica.</param>
    /// <returns>Lista de empresas asociadas al empleado.</returns>
    Task<IEnumerable<Empresa>> ObtenerEmpresaDetallesPorEmpleadoIdAsync(Guid empleadoId,
        CancellationToken cancellationToken);

    Task<int> ObtenerConteoDeEmpleadosPorEmpresaIdAsync(Guid empresaId, CancellationToken cancellationToken);

    Task<int> ObtenerConteoDeProyectosPorEmpresaAsync(Guid empresaId, CancellationToken cancellationToken);
}