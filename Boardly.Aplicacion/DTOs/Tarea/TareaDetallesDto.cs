using Boardly.Aplicacion.DTOs.Usuario;

namespace Boardly.Aplicacion.DTOs.Tarea;

public sealed record TareaDetallesDto
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
    Guid ActividadId,
    UsuarioFotoPerfilDto UsuarioFotoPerfil,
    bool EnRevision
);