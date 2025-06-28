namespace Boardly.Aplicacion.DTOs.Codigo;

public sealed record CodigoDto
(
    Guid CodigoId,
    Guid UsuarioId,
    string Codigo,
    bool Usado,
    DateTime? Expiracion
);