using Boardly.Dominio.Enum;

namespace Boardly.Aplicacion.DTOs.Usuario;

public record UsuarioDto(
    Guid UsuarioId,
    string Nombre,
    string Correo,
    string? NombreUsuario,
    DateTime FechaCreacion,
    EstadoUsuario Estado,
    string? FotoPerfil,
    DateTime? FechaRegistro
);
