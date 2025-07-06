namespace Boardly.Aplicacion.DTOs.Empleado;

public sealed record EmpleadoDto
(
    Guid EmpleadoId,
    Guid UsuarioId,
    Guid EmpresaId
);