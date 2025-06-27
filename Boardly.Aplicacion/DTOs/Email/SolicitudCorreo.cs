namespace Boardly.Aplicacion.DTOs.Email;

public sealed record SolicitudCorreo(
    string? To,

    string? Body, 

    string? Subject
);