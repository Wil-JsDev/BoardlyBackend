namespace Boardly.Aplicacion.DTOs.Usuario;

public sealed record ActualizarUsuarioDto
(
    string NombreUsuario,
    string Correo
);