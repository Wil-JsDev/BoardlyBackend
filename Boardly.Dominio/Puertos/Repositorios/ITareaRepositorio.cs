using Boardly.Dominio.Enum;
using Boardly.Dominio.Modelos;
using Boardly.Dominio.Utilidades;

namespace Boardly.Dominio.Puertos.Repositorios;

/// <summary>
/// Define los métodos específicos para acceder a datos de tareas.
/// </summary>
public interface ITareaRepositorio : IGenericoRepositorio<Tarea>
{
    Task<Tarea?> ObtenerConEmpleadosAsync(Guid tareaId, CancellationToken cancellationToken);

    /// <summary>
    /// Verifica si existe una tarea con el mismo nombre en un proyecto determinado.
    /// </summary>
    /// <param name="nombreTarea">Nombre de la tarea a verificar.</param>
    /// <param name="proyectoId">ID del proyecto asociado.</param>
    /// <param name="cancellationToken">Token de cancelación.</param>
    /// <returns>True si ya existe una tarea con ese nombre, false en caso contrario.</returns>
    Task<bool> ExisteNombreTareaAsync(string nombreTarea, Guid proyectoId, CancellationToken cancellationToken);

    /// <summary>
    /// Verifica si el nombre de una tarea ya está en uso por otra tarea en el mismo proyecto.
    /// </summary>
    /// <param name="nombreTarea">Nombre a verificar.</param>
    /// <param name="proyectoId">ID del proyecto.</param>
    /// <param name="tareaId">ID de la tarea actual (para excluirla de la verificación).</param>
    /// <param name="cancellationToken">Token de cancelación.</param>
    /// <returns>True si el nombre está en uso por otra tarea, false en caso contrario.</returns>
    Task<bool> NombreTareaEnUso(string nombreTarea, Guid proyectoId, Guid tareaId, CancellationToken cancellationToken);

    /// <summary>
    /// Obtiene todas las tareas asociadas a un proyecto.
    /// </summary>
    /// <param name="proyectoId">ID del proyecto.</param>
    /// <param name="cancellationToken">Token de cancelación.</param>
    /// <returns>Lista de tareas del proyecto.</returns>
    Task<IEnumerable<Tarea>> ObtenerTareasPorProyectoIdAsync(Guid proyectoId, CancellationToken cancellationToken);

    /// <summary>
    /// Obtiene las tareas de un proyecto filtradas por estado.
    /// </summary>
    /// <param name="proyectoId">ID del proyecto.</param>
    /// <param name="estado">Estado de las tareas a filtrar.</param>
    /// <param name="cancellationToken">Token de cancelación.</param>
    /// <returns>Lista de tareas que coinciden con el estado especificado.</returns>
    Task<IEnumerable<Tarea>> ObtenerTareasPorProyectoIdYEstadoAsync(Guid proyectoId, EstadoTarea estado, CancellationToken cancellationToken);

    /// <summary>
    /// Obtiene las tareas del usuario filtradas por la fecha de creación.
    /// </summary>
    /// <param name="proyectoId"></param>
    /// <param name="cancellationToken">Token de cancelación.</param>
    /// <returns>Lista de tareas creadas en el rango especificado.</returns>
    Task<List<Tarea>> ObtenerPorFechaCreacionAsync(Guid proyectoId,
        CancellationToken cancellationToken);

    /// <summary>
    /// Obtiene las tareas del usuario que vencen en un rango de tiempo relativo (hoy, esta semana, próximas 24 horas).
    /// </summary>
    /// <param name="hasta"></param>
    /// <param name="cancellationToken">Token de cancelación.</param>
    /// <param name="proyectoId"></param>
    /// <param name="desde"></param>
    /// <returns>Lista de tareas que cumplen con el criterio de vencimiento relativo.</returns>
    Task<List<Tarea>> ObtenerPorFechaVencimientoAsync(
        Guid proyectoId,
        DateTime desde,
        DateTime hasta,
        CancellationToken cancellationToken);

    /// <summary>
    /// Verifica si al agregar una dependencia entre dos tareas se genera una dependencia circular.
    /// </summary>
    /// <param name="tareaId">ID de la tarea que dependerá de otra.</param>
    /// <param name="dependeDeId">ID de la tarea de la cual se desea depender.</param>
    /// <param name="cancellationToken">Token de cancelación.</param>
    /// <returns>True si existe un ciclo, False si es seguro agregar la dependencia.</returns>
    Task<bool> ExisteDependenciaCircularAsync(Guid tareaId, Guid dependeDeId, CancellationToken cancellationToken);

    /// <summary>
    /// Verifica si una tarea existe en la base de datos por su ID.
    /// </summary>
    /// <param name="tareaId">ID de la tarea a verificar.</param>
    /// <param name="cancellationToken">Token para cancelar la operación si es necesario.</param>
    /// <returns>
    /// True si la tarea existe; de lo contrario, false.
    /// </returns>
    Task<bool> ExisteTareaPorIdAsync(Guid tareaId, CancellationToken cancellationToken);
    
    /// <summary>
    /// Obtiene los detalles completos de una tarea específica por su ID,
    /// incluyendo relaciones como usuarios asignados, comentarios y dependencias.
    /// </summary>
    /// <param name="tareaId">ID de la tarea a buscar.</param>
    /// <param name="cancellationToken">Token para cancelar la operación si es necesario.</param>
    /// <returns>
    /// Lista con la tarea correspondiente y sus relaciones cargadas, o vacía si no se encuentra.
    /// </returns>
    Task<List<Tarea>> ObtenerTareaPorIdDetalles(Guid tareaId, CancellationToken cancellationToken);


    /// <summary>
    /// Obtiene todas las tareas asignadas a un usuario específico.
    /// </summary>
    /// <param name="usuarioId">ID del usuario cuyas tareas se desean obtener.</param>
    /// <param name="cancellationToken">Token para cancelar la operación si es necesario.</param>
    /// <returns>
    /// Lista de tareas en las que el usuario está asignado.
    /// </returns>
    Task<List<Tarea>> ObtenerTareasPorUsuarioIdAsync(Guid usuarioId, CancellationToken cancellationToken);

    /// <summary>
    /// Obtiene una lista paginada de tareas asociadas a una actividad específica.
    /// </summary>
    /// <param name="actividadId">ID de la actividad.</param>
    /// <param name="numeroPagina">Número de la página a recuperar.</param>
    /// <param name="tamanioPagina">Cantidad de elementos por página.</param>
    /// <param name="cancellationToken">Token para cancelar la operación asincrónica.</param>
    /// <returns>Resultado paginado de tareas.</returns>
    Task<ResultadoPaginado<Tarea>> ObtenerPaginadoTareaAsync(Guid actividadId, int numeroPagina, int tamanioPagina, CancellationToken cancellationToken);

    /// <summary>
    /// Obtiene la cantidad de tareas agrupadas por estado dentro de un proyecto.
    /// </summary>
    /// <param name="proyectoId">ID del proyecto.</param>
    /// <param name="cancellationToken">Token para cancelar la operación asincrónica.</param>
    /// <returns>Número total de tareas agrupadas por estado.</returns>
    Task<int> ObtenerNumeroDeEstadoDeTareaPorProyectoId(Guid proyectoId, CancellationToken cancellationToken);

    /// <summary>
    /// Obtiene el número total de tareas asociadas a un proyecto.
    /// </summary>
    /// <param name="proyectoId">ID del proyecto.</param>
    /// <param name="cancellationToken">Token para cancelar la operación asincrónica.</param>
    /// <returns>Número total de tareas del proyecto.</returns>
    Task<int> ObtenerNumeroTareasPorProyectoIdAsync(Guid proyectoId, CancellationToken cancellationToken);

    Task<Tarea?> ObtenerDetallesPorTareaIdAsync(Guid tareaId, CancellationToken cancellationToken);

    Task<ResultadoPaginado<Tarea>> ObtenerTareasNoTerminadasEnPlazoDeTiempoAsync(Guid proyectoId,
        int numeroPagina,
        int tamanoPagina,
        CancellationToken cancellationToken
    );
}
