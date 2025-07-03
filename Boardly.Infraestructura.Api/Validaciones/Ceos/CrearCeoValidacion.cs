using Boardly.Aplicacion.DTOs.Ceo;
using FluentValidation;

namespace Boardly.Infraestructura.Api.Validaciones.Ceos;

public class CrearCeoValidacion : AbstractValidator<CrearCeoDto>
{
    public CrearCeoValidacion()
    {
        RuleFor(x => x.UsuarioId)
            .NotEmpty().WithMessage("El ID del usuario es obligatorio.");
    }
}
