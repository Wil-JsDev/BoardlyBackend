using Boardly.Dominio.Modelos;
using Boardly.Dominio.Utilidades;

namespace Boardly.Dominio.Puertos.Repositorios;
/// <summary>
/// Repositorio específico para operaciones relacionadas con la entidad <see cref="Actividad"/>.
/// </summary>
public interface IActividadRepositorio : IGenericoRepositorio<Actividad>
{
    /// <summary>
    /// Obtiene una lista paginada de actividades asociadas a un proyecto específico.
    /// </summary>
    /// <param name="proyectoId">ID del proyecto.</param>
    /// <param name="numeroPagina">Número de la página a recuperar.</param>
    /// <param name="tamanoPagina">Cantidad de elementos por página.</param>
    /// <param name="cancellationToken">Token para cancelar la operación asincrónica.</param>
    /// <returns>Resultado paginado de actividades.</returns>
    Task<ResultadoPaginado<Actividad>> ObtenerPaginasActividadByIdProyectoAsync(Guid proyectoId, int numeroPagina, int tamanoPagina, CancellationToken cancellationToken);

    /// <summary>
    /// Verifica si ya existe una actividad con el nombre especificado.
    /// </summary>
    /// <param name="nombreEmpresa">Nombre de la actividad a verificar.</param>
    /// <param name="cancellationToken">Token para cancelar la operación asincrónica.</param>
    /// <returns>True si existe una actividad con ese nombre, de lo contrario false.</returns>
    Task<bool> ExisteNombreActividadAsync(string nombreEmpresa, CancellationToken cancellationToken);

    /// <summary>
    /// Verifica si el nombre de una actividad está en uso por otra actividad diferente a la especificada.
    /// </summary>
    /// <param name="nombreUsuario">Nombre de la actividad a verificar.</param>
    /// <param name="actividadId">ID de la actividad actual (para excluirla).</param>
    /// <param name="cancellationToken">Token para cancelar la operación asincrónica.</param>
    /// <returns>True si el nombre está en uso por otra actividad, de lo contrario false.</returns>
    Task<bool> NombreActividadEnUso(string nombreUsuario, Guid actividadId, CancellationToken cancellationToken);

    /// <summary>
    /// Obtiene la cantidad total de actividades asociadas a un proyecto.
    /// </summary>
    /// <param name="proyectoId">ID del proyecto.</param>
    /// <param name="cancellationToken">Token para cancelar la operación asincrónica.</param>
    /// <returns>Número total de actividades del proyecto.</returns>
    Task<int> ObtenerNumeroActividadesPorProyectoIdAsync(Guid proyectoId, CancellationToken cancellationToken);
}