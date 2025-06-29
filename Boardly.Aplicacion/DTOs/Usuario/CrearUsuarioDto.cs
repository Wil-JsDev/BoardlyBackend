using Boardly.Dominio.Enum;
using Microsoft.AspNetCore.Http;

namespace Boardly.Aplicacion.DTOs.Usuario;

public record CrearUsuarioDto(
    string Nombre,
    string Apellido,
    string Correo,
    string? NombreUsuario,
    string? Contrasena,
    DateTime FechaCreacion,
    string Estado,
    IFormFile? FotoPerfil,
    DateTime? FechaRegistro
);
