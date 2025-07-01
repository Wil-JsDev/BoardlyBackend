using Asp.Versioning;
using Boardly.Infraestructura.Api.ManejadorDeExcepciones;
using Boardly.Infraestructura.Api.Validaciones;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace Boardly.Infraestructura.Api.ServiciosDeExtensiones;

public static class ServicioExtensiones
{
    
    public static void AgregarExcepciones(this IServiceCollection servicio)
    {
        servicio.AddExceptionHandler<ManejadorExcepcionesGlobal>();
    }

    public static void AgregarValidaciones(this IServiceCollection servicio)
    {
        servicio.AddFluentValidationAutoValidation();
        
        servicio.AddValidatorsFromAssemblyContaining<CrearUsuarioValidacion>();
    }
    public static void AgregarVersionado(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified =
                true; //When no versions are sent, this assumes the default version which is V1
            options.ReportApiVersions = true;
        });
    }
}
