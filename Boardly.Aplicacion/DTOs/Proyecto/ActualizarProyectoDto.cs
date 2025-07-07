namespace Boardly.Aplicacion.DTOs.Proyecto;

public sealed record ActualizarProyectoDto
(
    Guid ProyectoId,
    string Nombre,
    string? Descripcion
);