namespace Boardly.Aplicacion.DTOs.Ceo;

public sealed record ConteoEmpleadosCeoDto
(
    int TotalEmpleados,
    int EmpleadosActivos,
    int EmpleadosInactivos
);