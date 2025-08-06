using Boardly.Aplicacion.DTOs.Empresa;
using FluentValidation;

namespace Boardly.Infraestructura.Api.Validaciones.Empresas;

public class CrerEmpresaValidacion : AbstractValidator<CrearEmpresaDto>
{
    public CrerEmpresaValidacion()
    {
        RuleFor(x => x.Nombre)
            .NotEmpty().WithMessage("El nombre de la empresa es obligatorio.")
            .MaximumLength(100).WithMessage("El nombre no puede exceder los 100 caracteres.");

        RuleFor(x => x.Descripcion)
            .MaximumLength(250).WithMessage("La descripción no puede exceder los 250 caracteres.");

        RuleFor(x => x.CeoId)
            .NotEmpty().WithMessage("Debe proporcionarse el ID del CEO."); 

    }
}