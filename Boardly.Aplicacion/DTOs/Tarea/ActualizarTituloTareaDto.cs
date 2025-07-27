namespace Boardly.Aplicacion.DTOs.Tarea;

public sealed record ActualizarTituloTareaDto(Guid UsuarioId,string Titulo, List<Guid> EmpleadoIds);