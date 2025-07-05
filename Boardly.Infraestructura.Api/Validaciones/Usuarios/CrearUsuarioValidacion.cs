using Boardly.Aplicacion.DTOs.Usuario;
using FluentValidation;

namespace Boardly.Infraestructura.Api.Validaciones.Usuarios;
public class CrearUsuarioValidacion : AbstractValidator<CrearUsuarioDto>
{
    public CrearUsuarioValidacion()
    {
        RuleFor(x => x.Nombre)
            .NotEmpty().WithMessage("El nombre es obligatorio.")
            .MaximumLength(50).WithMessage("El nombre no puede tener más de 50 caracteres.");

        RuleFor(x => x.Apellido)
            .NotEmpty().WithMessage("El apellido es obligatorio.")
            .MaximumLength(50).WithMessage("El apellido no puede tener más de 50 caracteres.");

        RuleFor(x => x.Correo)
            .NotEmpty().WithMessage("El correo es obligatorio.")
            .EmailAddress().WithMessage("El correo no tiene un formato válido.")
            .MaximumLength(150).WithMessage("El correo no puede tener más de 150 caracteres.");

        RuleFor(x => x.NombreUsuario)
            .MaximumLength(30).WithMessage("El nombre de usuario no puede tener más de 30 caracteres.")
            .When(x => !string.IsNullOrWhiteSpace(x.NombreUsuario));

        RuleFor(x => x.Contrasena)
            .MinimumLength(6).WithMessage("La contraseña debe tener al menos 6 caracteres.")
            .When(x => !string.IsNullOrWhiteSpace(x.Contrasena));

        RuleFor(x => x.FotoPerfil)
            .Must(file => file == null || file.Length <= 5 * 1024 * 1024)
            .WithMessage("La foto de perfil no puede superar los 5MB.");
    }
}