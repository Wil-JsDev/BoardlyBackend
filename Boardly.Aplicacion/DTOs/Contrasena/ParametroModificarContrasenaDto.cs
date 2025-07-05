namespace Boardly.Aplicacion.DTOs.Contrasena;

public sealed record ParametroModificarContrasenaDto
(
    string? Codigo,
    string? Contrasena,
    string? ConfirmacionDeContrsena
);