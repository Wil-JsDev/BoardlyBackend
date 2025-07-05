namespace Boardly.Aplicacion.DTOs.Empresa;

public sealed record CrearEmpresaDto
(
    Guid? CeoId,
    Guid EmpleadoId,
    string Nombre,
    string? Descripcion
);