using Boardly.Aplicacion.DTOs.Empleado;
using FluentValidation;

namespace Boardly.Infraestructura.Api.Validaciones.Empleados;

public class CrearEmpleadoValidacion : AbstractValidator<CrearEmpleadoDto>
{
    public CrearEmpleadoValidacion()
    {
        RuleFor(x => x.UsuarioId)
            .NotEmpty().WithMessage("El ID del usuario es obligatorio.");
    }
}