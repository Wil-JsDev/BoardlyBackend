using Asp.Versioning;
using Boardly.Aplicacion.DTOs.Proyecto;
using Boardly.Dominio.Puertos.CasosDeUso.PDF;
using Microsoft.AspNetCore.Mvc;

namespace Boardly.Infraestructura.Api.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/generate-pdf")]
public class GeneradorPdfControlador(
    IGenerarReporte<ProyectoPaginacionParametroDto> generarReporte
    ) : ControllerBase
{
    [HttpPost("finished-project")]
    public async Task<IActionResult> GenerarReporteDeProyectosFinalizados(
        [FromBody] ProyectoPaginacionParametroDto solicitud,
        CancellationToken cancellationToken)
    {
        var resultado = await generarReporte.GenerarReporteProyectosFinalizadosAsync(solicitud, cancellationToken);

        if (!resultado.EsExitoso)
        {
            return BadRequest(resultado.Error);
        }

        return File(
            fileContents: resultado.Valor,
            contentType: "application/pdf",
            fileDownloadName: $"reporte_proyectos_{DateTime.UtcNow:yyyyMMdd_HHmmss}.pdf"
        );
    }
}