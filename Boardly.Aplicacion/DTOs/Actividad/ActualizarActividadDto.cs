using Boardly.Dominio.Enum;

namespace Boardly.Aplicacion.DTOs.Actividad;

public sealed record ActualizarActividadDto
(
    string Nombre,
    string Descripcion,
    EstadoActividad Estado,
    DateTime FechaInicio,
    DateTime FechaFin
);