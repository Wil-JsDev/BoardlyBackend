namespace Boardly.Aplicacion.DTOs.Contrasena;

public sealed record ParametroDeContrasena
(
    string ContrasenaAntigua,
    string NuevaContrasena,
    string ConfirmacionDeContrsena
);