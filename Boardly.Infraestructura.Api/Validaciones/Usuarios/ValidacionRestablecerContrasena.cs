using Boardly.Aplicacion.DTOs.Contrasena;
using FluentValidation;

namespace Boardly.Infraestructura.Api.Validaciones.Usuarios;

public class ValidacionRestablecerContrasena : AbstractValidator<RestablecerContrasenaDto>
{
    public ValidacionRestablecerContrasena()
    {
        RuleFor(x => x.UsuarioId)
            .NotEmpty().WithMessage("El ID del usuario es obligatorio.");

        RuleFor(x => x.ContrasenaAntigua)
            .NotEmpty().WithMessage("La contraseña antigua es obligatoria.");

        RuleFor(x => x.NuevaContrasena)
            .NotEmpty().WithMessage("La nueva contraseña es obligatoria.")
            .MinimumLength(10).WithMessage("La nueva contraseña debe tener al menos 10 caracteres.")
            .NotEqual(x => x.ContrasenaAntigua).WithMessage("La nueva contraseña no debe ser igual a la anterior.");

        RuleFor(x => x.ConfirmacionDeContrsena)
            .Equal(x => x.NuevaContrasena).WithMessage("La confirmación de la contraseña no coincide con la nueva contraseña.");
    }
}