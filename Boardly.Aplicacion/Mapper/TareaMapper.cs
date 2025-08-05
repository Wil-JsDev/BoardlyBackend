using Boardly.Aplicacion.DTOs.Comentarios;
using Boardly.Aplicacion.DTOs.Tarea;
using Boardly.Dominio.Modelos;

namespace Boardly.Aplicacion.Mapper;

public static class TareaMapper
{
    public static TareaDetalles MapToDetalles(Tarea tarea)
    {
        var primerComentario = tarea.Comentarios.FirstOrDefault();
        var usuarioComentario = primerComentario?.Usuario;

        var comentarioDto = primerComentario is null || usuarioComentario is null
            ? new ComentarioDto(Guid.Empty, string.Empty, new UsuarioDetallesComentarioDto(Guid.Empty, string.Empty, null))
            : new ComentarioDto(
                ComentarioId: primerComentario.ComentarioId,
                Texto: primerComentario.Contenido,
                Usuario: new UsuarioDetallesComentarioDto(
                    UsuarioId: usuarioComentario.UsuarioId,
                    NombreCompleto: $"{usuarioComentario.Nombre} {usuarioComentario.Apellido}",
                    FotoPerfil: usuarioComentario.FotoPerfil
                )
            );

        var empleadosDto = tarea.TareasEmpleado?
            .Where(te => te.Empleado != null && te.Empleado.Usuario != null)
            .Select(te => new EmpleadoTareaDto(
                EmpleadoId: te.EmpleadoId,
                NombreCompleto: $"{te.Empleado.Usuario.Nombre} {te.Empleado.Usuario.Apellido}",
                Rol: te.Empleado.EmpleadosProyectoRol.FirstOrDefault()?.RolProyecto.Nombre ?? string.Empty,
                fotoPerfil: te.Empleado.Usuario.FotoPerfil!
            ))
            .ToList() ?? new List<EmpleadoTareaDto>();

        return new TareaDetalles(
            tarea.Titulo,
            tarea.Estado,
            tarea.Descripcion,
            tarea.Archivo,
            tarea.FechaVencimiento,
            tarea.FechaInicio,
            comentarioDto,
            empleadosDto
        );
    }

}
