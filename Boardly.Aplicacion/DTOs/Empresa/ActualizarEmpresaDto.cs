namespace Boardly.Aplicacion.DTOs.Empresa;

public sealed record ActualizarEmpresaDto
(
    string Nombre,
    string? Descripcion,
    string Estado
);