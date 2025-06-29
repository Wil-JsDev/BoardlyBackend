namespace Boardly.Aplicacion.DTOs.Email;

public sealed record SolicitudCorreo(
    string? Destinatario,
    string? Cuerpo,
    string? Asunto
);