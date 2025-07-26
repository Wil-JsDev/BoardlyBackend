namespace Boardly.Aplicacion.DTOs.Proyecto;

public record ProyectoConteoDto
(
    int ConteoActividades,
    int ConteoTareas,
    int ConteoTareasCompletadas,
    int ConteoTareasPendientes
);