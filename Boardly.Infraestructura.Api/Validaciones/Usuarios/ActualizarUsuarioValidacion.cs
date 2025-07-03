using Boardly.Aplicacion.DTOs.Usuario;
using FluentValidation;

namespace Boardly.Infraestructura.Api.Validaciones.Usuarios;

public class ActualizarUsuarioValidacion : AbstractValidator<ActualizarUsuarioDto>
{
    public ActualizarUsuarioValidacion()
    {
        RuleFor(x => x.NombreUsuario)
            .MaximumLength(30).WithMessage("El nombre de usuario no puede tener más de 30 caracteres.")
            .When(x => !string.IsNullOrWhiteSpace(x.NombreUsuario));

        RuleFor(x => x.Correo)
            .NotEmpty().WithMessage("El correo es obligatorio.")
            .EmailAddress().WithMessage("El correo no tiene un formato válido.")
            .MaximumLength(150).WithMessage("El correo no puede tener más de 150 caracteres.");
    }
}
