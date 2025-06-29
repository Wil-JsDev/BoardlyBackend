namespace Boardly.Aplicacion.DTOs.Empresa;

public sealed record CrearEmpresaDto
(
    Guid? CeoId,
    string Nombre,
    string? Descripcion
        
);