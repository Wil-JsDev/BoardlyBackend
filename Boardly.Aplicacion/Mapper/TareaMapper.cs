using Boardly.Aplicacion.DTOs.Comentarios;
using Boardly.Aplicacion.DTOs.Tarea;
using Boardly.Dominio.Modelos;

namespace Boardly.Aplicacion.Mapper;

public static class TareaMapper
{
    public static TareaDetalles MapToDetalles(Tarea tarea)
    {
        var primerComentario = tarea.Comentarios.FirstOrDefault();
        var primerEmpleadoTarea = tarea.TareasEmpleado?.FirstOrDefault();
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

        var usuarioEmpleado = primerEmpleadoTarea?.Empleado?.Usuario;

        var empleadoDto = primerEmpleadoTarea is null || primerEmpleadoTarea.Empleado is null || usuarioEmpleado is null
            ? new EmpleadoTareaDto(Guid.Empty, string.Empty, string.Empty, null)
            : new EmpleadoTareaDto(
                EmpleadoId: primerEmpleadoTarea.EmpleadoId,
                NombreCompleto: $"{usuarioEmpleado.Nombre} {usuarioEmpleado.Apellido}",
                Rol: primerEmpleadoTarea.Empleado.EmpleadosProyectoRol.FirstOrDefault()?.RolProyecto.Nombre ?? string.Empty,
                fotoPerfil: usuarioEmpleado.FotoPerfil!
                
            );

        return new TareaDetalles(
            tarea.Titulo,
            tarea.Estado,
            tarea.Descripcion,
            tarea.FechaVencimiento,
            tarea.FechaInicio,
            comentarioDto,
            empleadoDto
        );
    }

}
