namespace Boardly.Aplicacion.DTOs.Empresa;

public record EmpresaProyectosDto
(
    Guid EmpresaId,
    Guid? CeoId,
    string Nombre,
    string? Descripcion,
    DateTime FechaCreacion,
    string Estado,
    EmpresaConteoDto EmpresaConteo
);