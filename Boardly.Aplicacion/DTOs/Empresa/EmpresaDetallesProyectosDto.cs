namespace Boardly.Aplicacion.DTOs.Empresa;

public sealed record EmpresaDetallesProyectosDto
(
    Guid EmpresaId,
    Guid? CeoId,
    string Nombre,
    string? Descripcion,
    string Rol,
    int ActividadesCount,
    int TareasCount,
    int EstadoTareaCount,
    DateTime FechaFinalizacion,
    string Estado
);