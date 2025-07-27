namespace Boardly.Aplicacion.DTOs.Tarea;

public record CrearTareaDto(
    Guid ProyectoId,
    Guid UsuarioId,
    List<Guid> EmpleadoIds,
    string Titulo,
    string? Descripcion,
    DateTime FechaVencimiento,
    DateTime FechaInicio,
    Guid ActividadId
);