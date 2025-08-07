namespace Boardly.Aplicacion.DTOs.Tarea;


public sealed record TareasNoFinalizadasPdf
(
    DateTime FechaGeneracion,
    List<TareaNoFinalizadaDto> Tareas
);

public sealed record TareaNoFinalizadaDto
(
    string Nombre,
    DateTime FechaVencimiento,
    string Estado
);
