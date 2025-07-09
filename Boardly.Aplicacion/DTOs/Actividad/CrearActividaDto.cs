using Boardly.Dominio.Enum;

namespace Boardly.Aplicacion.DTOs.Actividad;

public sealed record CrearActividaDto
(
    Guid ProyectoId,   
    string Nombre,
    ActividadPrioridad Prioridad,
    string Descripcion,
    EstadoActividad Estado,
    DateTime FechaInicio,
    DateTime FechaFin,
    int Orden
);