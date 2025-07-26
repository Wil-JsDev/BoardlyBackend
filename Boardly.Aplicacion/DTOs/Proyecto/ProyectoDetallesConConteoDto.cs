namespace Boardly.Aplicacion.DTOs.Proyecto;

public record ProyectoDetallesConConteoDto
(
    Guid? ProyectoId,
    Guid EmpresaId,
    string Nombre,
    string? Descripcion,
    DateTime FechaInicio,
    DateTime? FechaFin,
    string Estado,
    DateTime FechaCreado,
    ProyectoConteoDto ProyectoConteo
);