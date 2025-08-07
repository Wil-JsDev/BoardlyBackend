namespace Boardly.Aplicacion.DTOs.Tarea;

public record ParametroPaginacionTareaDto
(
    Guid ProyectoId,
    int NumeroPagina,
    int TamanoPagina
);