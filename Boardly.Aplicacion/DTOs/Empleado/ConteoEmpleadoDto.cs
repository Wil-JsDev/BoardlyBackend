namespace Boardly.Aplicacion.DTOs.Empleado;

public sealed record ConteoEmpleadoDto
(
    int Empresas,
    int Proyectos,
    int Actividades,
    int TareasTotales,
    int TareasEnProceso,
    int TareasApuntoDeVencer
);