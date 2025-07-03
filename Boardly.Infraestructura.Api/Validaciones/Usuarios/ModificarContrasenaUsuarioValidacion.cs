using Boardly.Aplicacion.DTOs.Contrasena;
using FluentValidation;

namespace Boardly.Infraestructura.Api.Validaciones.Usuarios;

public class ModificarContrasenaUsuarioValidacion : AbstractValidator<ModificarContrasenaUsuarioDto>
{
    public ModificarContrasenaUsuarioValidacion()
    {
        RuleFor(x => x.UsuarioId)
            .NotEmpty().WithMessage("El ID del usuario es obligatorio.");

        RuleFor(x => x.Codigo)
            .NotEmpty().WithMessage("El código de verificación es obligatorio.")
            .MaximumLength(100).WithMessage("El código no puede exceder los 100 caracteres.");

        RuleFor(x => x.Contrasena)
            .NotEmpty().WithMessage("La contraseña es obligatoria.")
            .MinimumLength(15).WithMessage("La contraseña debe tener al menos 15 caracteres.");

        RuleFor(x => x.ConfirmacionDeContrsena)
            .NotEmpty().WithMessage("La confirmación de la contraseña es obligatoria.")
            .Equal(x => x.Contrasena).WithMessage("La confirmación de la contraseña no coincide.");
    }
}
