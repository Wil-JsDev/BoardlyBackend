using Boardly.Aplicacion.DTOs.Proyecto;
using FluentValidation;

namespace Boardly.Infraestructura.Api.Validaciones.Proyectos;

public class ActualizarProyectoValidacion : AbstractValidator<ActualizarProyectoDto>
{
    public ActualizarProyectoValidacion()
    {

        RuleFor(x => x.Nombre)
            .NotEmpty().WithMessage("El nombre del proyecto es obligatorio.")
            .MaximumLength(100).WithMessage("El nombre no puede superar los 100 caracteres.");

        RuleFor(x => x.Descripcion)
            .MaximumLength(500).WithMessage("La descripción no puede superar los 500 caracteres.");
    }
}
