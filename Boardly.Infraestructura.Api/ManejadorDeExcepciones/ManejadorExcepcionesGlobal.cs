using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Boardly.Infraestructura.Api.ManejadorDeExcepciones;

public class ManejadorExcepcionesGlobal : IExceptionHandler
{
    private readonly ILogger<ManejadorExcepcionesGlobal> _logger;

    public ManejadorExcepcionesGlobal(ILogger<ManejadorExcepcionesGlobal> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken
    )
    {
        var detallesProblema = new ProblemDetails
        {
            Instance = httpContext.Request.Path
        };

        if (exception is FluentValidation.ValidationException excepcionValidacion)
        {
            detallesProblema.Title = "Ocurrieron uno o más errores de validación.";
            detallesProblema.Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1";
            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

            List<string> erroresValidacion = new();
            foreach (var error in excepcionValidacion.Errors)
            {
                erroresValidacion.Add(error.ErrorMessage);
            }

            detallesProblema.Extensions.Add("Errores", erroresValidacion);
        }
        else
        {
            detallesProblema.Title = "Ha ocurrido un error inesperado. Intenta más tarde.";
            detallesProblema.Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1";
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

            _logger.LogError(exception, "Excepción no controlada: {Message}", exception.Message);
        }

        detallesProblema.Status = httpContext.Response.StatusCode;

        await httpContext.Response.WriteAsJsonAsync(detallesProblema, cancellationToken)
            .ConfigureAwait(false);

        return true;
    }

}