namespace Boardly.Aplicacion.DTOs.Tarea;

public sealed record ActualizarTituloTareaDto(Guid UsuarioId, string Titulo,string Descripcion, List<Guid> EmpleadoIds);