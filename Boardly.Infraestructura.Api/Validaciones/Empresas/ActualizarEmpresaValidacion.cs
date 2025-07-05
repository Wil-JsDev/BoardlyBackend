using Boardly.Aplicacion.DTOs.Empresa;
using FluentValidation;

namespace Boardly.Infraestructura.Api.Validaciones.Empresas;

public class ActualizarEmpresaValidacion : AbstractValidator<ActualizarEmpresaDto>
{
    public ActualizarEmpresaValidacion()
    {
        RuleFor(x => x.Nombre)
            .NotEmpty().WithMessage("El nombre de la empresa es obligatorio.")
            .MaximumLength(100).WithMessage("El nombre no puede exceder los 100 caracteres.");

        RuleFor(x => x.Descripcion)
            .MaximumLength(250).WithMessage("La descripción no puede exceder los 250 caracteres.");

        RuleFor(x => x.Estado)
            .NotEmpty().WithMessage("El estado es obligatorio.")
            .Must(estado => estado == "Activo" || estado == "Inactivo")
            .WithMessage("El estado debe ser 'Activo' o 'Inactivo'.");
    }
}