namespace Boardly.Aplicacion.DTOs.Empresa;

public sealed record EmpresaDto
(
    Guid EmpresaId,
    Guid? CeoId,
    Guid EmpleadoId,
    string Nombre,
    string? Descripcion,
    DateTime FechaCreacion,
    string Estado
);