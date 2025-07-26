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

        var comentarioDto = primerComentario is null
            ? new ComentarioDto(Guid.Empty, string.Empty, new UsuarioDetallesComentarioDto(Guid.Empty, string.Empty, null))
            : new ComentarioDto(
                ComentarioId: primerComentario.ComentarioId,
                Texto: primerComentario.Contenido,
                Usuario: new UsuarioDetallesComentarioDto(
                    UsuarioId: primerComentario.UsuarioId,
                    NombreCompleto: $"{primerComentario.Usuario.Nombre} {primerComentario.Usuario.Apellido}",
                    FotoPerfil: primerComentario.Usuario.FotoPerfil
                )
            );

        var empleadoDto = primerEmpleadoTarea is null || primerEmpleadoTarea.Empleado is null
            ? new EmpleadoTareaDto(string.Empty, string.Empty)
            : new EmpleadoTareaDto(
                NombreCompleto: $"{primerEmpleadoTarea.Empleado.Usuario.Nombre} {primerEmpleadoTarea.Empleado.Usuario.Apellido}",
                Rol: primerEmpleadoTarea.Empleado.EmpleadosProyectoRol.FirstOrDefault()?.RolProyecto.Nombre ?? string.Empty
            );

        return new TareaDetalles(
            tarea.Titulo,
            tarea.Estado,
            tarea.Descripcion,
            tarea.FechaVencimiento,
            comentarioDto,
            empleadoDto
        );
    }
}
