namespace Boardly.Aplicacion.DTOs.Tarea;

public record TareaDto
(
    Guid TareaId,
    Guid ProyectoId,
    string Titulo,
    string? EstadoTarea,
    string? Descripcion,
    DateTime FechaInicio,
    DateTime FechaVencimiento,
    DateTime? FechaActualizacion,
    DateTime FechaCreado,
    Guid ActividadId
);