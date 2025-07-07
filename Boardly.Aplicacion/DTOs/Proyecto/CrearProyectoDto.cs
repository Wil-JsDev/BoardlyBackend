namespace Boardly.Aplicacion.DTOs.Proyecto;

public sealed record CrearProyectoDto
(
    Guid EmpresaId,    
    string Nombre,
    string? Descripcion,
    DateTime FechaInicio,
    DateTime? FechaFin,
    string Estado
);