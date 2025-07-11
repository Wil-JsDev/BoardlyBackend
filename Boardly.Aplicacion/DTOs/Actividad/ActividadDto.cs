namespace Boardly.Aplicacion.DTOs.Actividad;

public sealed record ActividadDto
(
    Guid ActividadId,
    Guid ProyectoId,  
    string Nombre,
    string Prioridad,
    string Descripcion,
    string Estado,
    DateTime FechaInicio,
    DateTime FechaFin,
    int Orden
);