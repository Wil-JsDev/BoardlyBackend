using Boardly.Aplicacion.DTOs.Usuario;
using FluentValidation;

namespace Boardly.Infraestructura.Api.Validaciones.Usuarios;

public class ActualizarUsuarioValidacion : AbstractValidator<ActualizarUsuarioDto>
{
    public ActualizarUsuarioValidacion()
    {
        RuleFor(x => x.Nombre)
            .MaximumLength(40).WithMessage("El nombre del usuario no puede tener más de 40 caracteres.")
            .When(x => !string.IsNullOrWhiteSpace(x.Nombre));

        RuleFor(x => x.Apellido)
            .NotEmpty().WithMessage("El Apellido es obligatorio.")
            .MaximumLength(40).WithMessage("El apellido del usuario no puede tener más de 40 caracteres.");


    }
}
