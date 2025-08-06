using Boardly.Aplicacion.DTOs.Proyecto;

namespace Boardly.Aplicacion.DTOs.Empresa;

public sealed record EmpresaDetallesProyectosDto
(
    string Nombre,
    string? Descripcion,
    string Rol,
    ProyectoEmpresaDto ProyectoEmpresa,
    int ActividadesCount,
    int TareasCount,
    int EstadoTareaCount,
    DateTime FechaFinalizacion,
    string Estado
);