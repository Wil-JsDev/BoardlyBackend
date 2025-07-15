using Boardly.Aplicacion.DTOs.Proyecto;
using Boardly.Dominio.Enum;
using FluentValidation;

namespace Boardly.Infraestructura.Api.Validaciones.Proyectos;

public class CrearProyectoValidacion : AbstractValidator<CrearProyectoDto>
{
    public CrearProyectoValidacion()
    {
        RuleFor(x => x.EmpresaId)
            .NotEmpty().WithMessage("El ID de la empresa es obligatorio.");

        RuleFor(x => x.Nombre)
            .NotEmpty().WithMessage("El nombre del proyecto es obligatorio.")
            .MaximumLength(100).WithMessage("El nombre no puede superar los 100 caracteres.");

        RuleFor(x => x.Descripcion)
            .MaximumLength(500).WithMessage("La descripción no puede superar los 500 caracteres.")
            .When(x => !string.IsNullOrWhiteSpace(x.Descripcion));

        RuleFor(x => x.FechaInicio)
            .NotEmpty().WithMessage("La fecha de inicio es obligatoria.")
            .Must(f => f > DateTime.MinValue).WithMessage("La fecha de inicio no es válida.");

        RuleFor(x => x.FechaFin)
            .Must((dto, fechaFin) => !fechaFin.HasValue || fechaFin > dto.FechaInicio)
            .WithMessage("La fecha de finalización debe ser posterior a la fecha de inicio.");

        RuleFor(x => x.Estado)
            .Must(value => Enum.IsDefined(typeof(EstadoProyecto), value))
            .WithMessage("El estado del proyecto es obligatorio y debe ser válido.");

    }
}