namespace Boardly.Aplicacion.DTOs.Actividad;

public sealed record ActividadDetallesDto
(
    Guid ActividadId,
    Guid ProyectoId,  
    string Nombre,
    string Prioridad,
    string Descripcion,
    string Estado,
    DateTime FechaInicio,
    DateTime FechaFin,
    int Orden,
    ActividadConteoDto ActividadConteo
);