using Asp.Versioning;
using Boardly.Infraestructura.Api.Middleware;
using Boardly.Infraestructura.Api.Validaciones.Autenticaciones;
using Boardly.Infraestructura.Api.Validaciones.Ceos;
using Boardly.Infraestructura.Api.Validaciones.Empleados;
using Boardly.Infraestructura.Api.Validaciones.Empresas;
using Boardly.Infraestructura.Api.Validaciones.Usuarios;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace Boardly.Infraestructura.Api.ServiciosDeExtensiones;

public static class ServicioExtensiones
{
    public static void AgregarMiddlewares(this IApplicationBuilder app)
    {
        app.UseMiddleware<MiddlewareCapturaExcepciones>();
    }

    public static void AgregarValidaciones(this IServiceCollection servicio)
    {
        servicio.AddFluentValidationAutoValidation();
        servicio.AddValidatorsFromAssemblyContaining<CrearUsuarioValidacion>();
        servicio.AddValidatorsFromAssemblyContaining<AutenticacionSolicitudValidacion>();
        servicio.AddValidatorsFromAssemblyContaining<CrearCeoValidacion>();
        servicio.AddValidatorsFromAssemblyContaining<CrearEmpleadoValidacion>();
        servicio.AddValidatorsFromAssemblyContaining<ActualizarEmpresaValidacion>();
        servicio.AddValidatorsFromAssemblyContaining<CrearEmpleadoValidacion>();
        servicio.AddValidatorsFromAssemblyContaining<ActualizarUsuarioValidacion>();
        servicio.AddValidatorsFromAssemblyContaining<ModificarContrasenaUsuarioValidacion>();
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
