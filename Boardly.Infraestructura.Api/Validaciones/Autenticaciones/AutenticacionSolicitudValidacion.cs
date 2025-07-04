using Boardly.Aplicacion.DTOs.Autenticacion;
using FluentValidation;

namespace Boardly.Infraestructura.Api.Validaciones.Autenticaciones;

public class AutenticacionSolicitudValidacion : AbstractValidator<AutenticacionSolicitud>
{
    public AutenticacionSolicitudValidacion()
    {
        RuleFor(x => x.Correo)
            .NotEmpty().WithMessage("El correo es obligatorio.")
            .EmailAddress().WithMessage("El correo no tiene un formato válido.")
            .MaximumLength(100).WithMessage("El correo no puede exceder los 100 caracteres.");

        RuleFor(x => x.Contrasena)
            .NotEmpty().WithMessage("La contraseña es obligatoria.")
            .MinimumLength(6).WithMessage("La contraseña debe tener al menos 6 caracteres.");
    }
}
