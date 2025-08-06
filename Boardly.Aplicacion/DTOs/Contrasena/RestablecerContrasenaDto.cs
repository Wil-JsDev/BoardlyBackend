namespace Boardly.Aplicacion.DTOs.Contrasena;

public sealed record RestablecerContrasenaDto
(
    Guid UsuarioId,
    string ContrasenaAntigua,
    string NuevaContrasena,
    string ConfirmacionDeContrsena
);