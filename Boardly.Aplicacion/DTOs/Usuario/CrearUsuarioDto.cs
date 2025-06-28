using Boardly.Dominio.Enum;
using Microsoft.AspNetCore.Http;

namespace Boardly.Aplicacion.DTOs.Usuario;

public record CrearUsuarioDto(
    string Nombre,
    string Correo,
    string? NombreUsuario,
    string? Contrasena,
    DateTime FechaCreacion,
    EstadoUsuario Estado,
    IFormFile? FotoPerfil,
    DateTime? FechaRegistro
);
