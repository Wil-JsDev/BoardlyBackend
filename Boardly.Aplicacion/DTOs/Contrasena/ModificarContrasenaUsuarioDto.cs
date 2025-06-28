namespace Boardly.Aplicacion.DTOs.Contrasena;

public sealed record ModificarContrasenaUsuarioDto
(
    Guid UsuarioId,
    string Codigo,
    string Contrasena,
    string ConfirmacionDeContrsena
);