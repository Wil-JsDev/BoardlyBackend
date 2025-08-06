using Boardly.Aplicacion.DTOs.Usuario;
using Microsoft.AspNetCore.Http;

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
    Guid ActividadId,
    List<UsuarioFotoPerfilDto> UsuarioFotoPerfil,
    string Archivo

);