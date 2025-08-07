namespace Boardly.Aplicacion.DTOs.Proyecto;

public record ProyectoPaginacionParametroDto
(
    Guid? EmpresaId,
    int NumeroPagina,
    int TamanoPagina
);