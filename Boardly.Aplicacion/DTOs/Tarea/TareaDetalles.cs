using Boardly.Aplicacion.DTOs.Comentarios;

namespace Boardly.Aplicacion.DTOs.Tarea;

public record TareaDetalles
(
    string Titulo,
    string? EstadoTarea,
    string? Descripcion,
    DateTime FechaVencimiento,
    ComentarioDto ComentarioDto,
    EmpleadoTareaDto EmpleadoTareaDto
);
public record EmpleadoTareaDto(Guid EmpleadoId,string NombreCompleto, string Rol);