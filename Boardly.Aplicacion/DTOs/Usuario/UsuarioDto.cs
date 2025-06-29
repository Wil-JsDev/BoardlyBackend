using Boardly.Dominio.Enum;

namespace Boardly.Aplicacion.DTOs.Usuario;

public record UsuarioDto(
    Guid UsuarioId,
    string Nombre,
    string Apellido,
    string Correo,
    string? NombreUsuario,
    DateTime FechaCreacion,
    string Estado,
    string? FotoPerfil,
    DateTime? FechaRegistro
);
