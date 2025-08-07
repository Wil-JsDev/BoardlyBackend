using Boardly.Aplicacion.DTOs.PDF;
using Boardly.Aplicacion.DTOs.Proyecto;
using Boardly.Dominio.Puertos.CasosDeUso.PDF;
using Boardly.Dominio.Puertos.Repositorios;
using Boardly.Dominio.Utilidades;
using Microsoft.Extensions.Logging;

namespace Boardly.Aplicacion.Adaptadores.PDF;

public class GenerarReporte(
    IProyectoRepositorio repositorioProyecto,
    IGeneradorPdf<ProyectoDatosPdf> generadorPdf,
    ILogger<GenerarReporte> logger
    ) : IGenerarReporte<ProyectoPaginacionParametroDto>
{
    public async Task<ResultadoT<byte[]>> GenerarReporteProyectosFinalizadosAsync(ProyectoPaginacionParametroDto solicitud, CancellationToken cancellationToken)
    {
        var proyectosFinalizados = await repositorioProyecto.ProyectosFinalizados(solicitud.EmpresaId ?? Guid.Empty,
            solicitud.NumeroPagina, solicitud.TamanoPagina, cancellationToken);

        if (!proyectosFinalizados.Elementos!.Any())
        {
            logger.LogInformation("");
            
            return ResultadoT<byte[]>.Fallo(Error.Fallo("400", ""));
        }
        
        var proyectosDto = proyectosFinalizados.Elementos!.Select(proyecto => new ProyectoDtoPdf(
            Nombre: proyecto.Nombre,
            Descripcion: proyecto.Descripcion,
            FechaInicio: proyecto.FechaInicio,
            FechaFin: proyecto.FechaFin
        )).ToList();

        var datosReporte = new ProyectoDatosPdf(
            Fecha: DateTime.UtcNow,
            ProyectoDto: proyectosDto
        );

        var pdfBytes = await generadorPdf.GenerarReporteAsync(datosReporte);

        logger.LogInformation("Reporte PDF de proyectos finalizados generado exitosamente para la empresa {EmpresaId}.",
            solicitud.EmpresaId);

        return ResultadoT<byte[]>.Exito(pdfBytes);
    }
}