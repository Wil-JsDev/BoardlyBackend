using Boardly.Aplicacion.DTOs.PDF;
using Boardly.Aplicacion.DTOs.Proyecto;
using Boardly.Dominio.Puertos.CasosDeUso.PDF;
using Boardly.Dominio.Puertos.Repositorios;
using Boardly.Dominio.Utilidades;
using Microsoft.Extensions.Logging;

namespace Boardly.Aplicacion.Adaptadores.PDF;

public class GenerarReporte(
    IProyectoRepositorio repositorioProyecto,
    IGeneradorPdf<List<ProyectoDatosPdf>> generadorPdf,
    ILogger<GenerarReporte> logger
    ) : IGenerarReporte<ProyectoPaginacionParametroDto>
{
    public async Task<ResultadoT<byte[]>> GenerarReporteAsync(ProyectoPaginacionParametroDto solicitud, CancellationToken cancellationToken)
    {
        var proyectosFinalizados = await repositorioProyecto.ProyectosFinalizados(solicitud.EmpresaId ?? Guid.Empty,
            solicitud.NumeroPagina, solicitud.TamanoPagina, cancellationToken);

        if (!proyectosFinalizados.Elementos!.Any())
        {
            logger.LogInformation("No se encontraron proyectos finalizados con los parámetros proporcionados.");
    
            return ResultadoT<byte[]>.Fallo(
                Error.Fallo("400", "No hay proyectos finalizados que coincidan con los criterios de búsqueda.")
            );
        }
        
        var proyectosDto = proyectosFinalizados.Elementos!.Select(proyecto => new ProyectoDtoPdf(
            Nombre: proyecto.Nombre,
            Descripcion: proyecto.Descripcion,
            FechaInicio: proyecto.FechaInicio,
            FechaFin: proyecto.FechaFin
        )).ToList();

        // Convertimos en lista porque el generador espera List<ProyectoDatosPdf>
        var datosReporte = new List<ProyectoDatosPdf>
        {
            new ProyectoDatosPdf(
                Fecha: DateTime.UtcNow,
                ProyectoDto: proyectosDto
            )
        };

        var pdfBytes = await generadorPdf.GenerarReporteAsync(datosReporte);

        logger.LogInformation("Reporte PDF de proyectos finalizados generado exitosamente para la empresa {EmpresaId}.",
            solicitud.EmpresaId);

        return ResultadoT<byte[]>.Exito(pdfBytes);
    }
}