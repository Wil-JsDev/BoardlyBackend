namespace Boardly.Aplicacion.DTOs.Empleado;

public sealed record EmpleadoResumenDto(
    Guid EmpleadoId,
    string Nombre,
    string Correo,
    Guid RolId,
    string NombreRol,
    Guid ProyectoId
    );