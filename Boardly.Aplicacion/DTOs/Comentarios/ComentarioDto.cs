namespace Boardly.Aplicacion.DTOs.Comentarios;

public sealed record ComentarioDto(Guid ComentarioId, string Texto, UsuarioDetallesComentarioDto Usuario);

public sealed record UsuarioDetallesComentarioDto(Guid UsuarioId, string NombreCompleto, string? FotoPerfil);