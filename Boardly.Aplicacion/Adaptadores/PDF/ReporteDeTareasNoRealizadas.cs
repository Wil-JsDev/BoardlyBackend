using Boardly.Aplicacion.DTOs.Tarea;
using Boardly.Dominio.Puertos.CasosDeUso.PDF;
using Boardly.Dominio.Puertos.Repositorios;
using Boardly.Dominio.Utilidades;
using Microsoft.Extensions.Logging;

namespace Boardly.Aplicacion.Adaptadores.PDF;

public class ReporteDeTareasNoRealizadas(
    IGeneradorPdf<IList<TareasNoFinalizadasPdf>> generadorPdf,
    IProyectoRepositorio proyectoRepositorio,
    ITareaRepositorio tareaRepositorio,
    ILogger<ReporteDeTareasNoRealizadas> logger
    ) : IGenerarReporte<ParametroPaginacionTareaDto>
{
    public async Task<ResultadoT<byte[]>> GenerarReporteAsync(ParametroPaginacionTareaDto solicitud, CancellationToken cancellationToken)
    {
        if (solicitud.NumeroPagina <= 0 || solicitud.TamanoPagina <= 0)
        {
            logger.LogInformation("Parámetros de paginación inválidos: Número de página {NumeroPagina}, Tamaño de página {TamanoPagina}.",
                solicitud.NumeroPagina, solicitud.TamanoPagina);

            return ResultadoT<byte[]>.Fallo(
                Error.Fallo("400", "Los parámetros de paginación deben ser mayores a cero.")
            );
        }

        var proyecto = await proyectoRepositorio.ObtenerByIdAsync(solicitud.ProyectoId, cancellationToken);
        if (proyecto is null)
        {
            logger.LogWarning("No se encontró el proyecto con ID {ProyectoId}.", solicitud.ProyectoId);

            return ResultadoT<byte[]>.Fallo(
                Error.NoEncontrado("404", $"No se encontró el proyecto con ID {solicitud.ProyectoId}.")
            );
        }

        var tareasNoRealizadas = await tareaRepositorio.ObtenerTareasNoTerminadasEnPlazoDeTiempoAsync
        (
            solicitud.ProyectoId,
            solicitud.NumeroPagina,
            solicitud.TamanoPagina,
            cancellationToken
        );

        if (!tareasNoRealizadas.Elementos!.Any())
        {
            logger.LogWarning("No se encontraron tareas no finalizadas dentro del plazo para el proyecto con ID {ProyectoId}.",
                solicitud.ProyectoId);

            return ResultadoT<byte[]>.Fallo(
                Error.Fallo("400", "No hay tareas no finalizadas que coincidan con los criterios de búsqueda.")
            );
        }

        var tareasNoFinalizadasDto = tareasNoRealizadas.Elementos!.Select(x =>
            new TareasNoFinalizadasPdf
            (
                FechaGeneracion: DateTime.UtcNow,
                Tareas:
                [
                    new TareaNoFinalizadaDto(
                        Nombre: x.Titulo,
                        FechaVencimiento: x.FechaVencimiento,
                        Estado: x.Estado
                    )
                ]
            )).ToList();

        var pdfBytes = await generadorPdf.GenerarReporteAsync(tareasNoFinalizadasDto);

        logger.LogInformation("Reporte PDF de tareas no finalizadas generado exitosamente para el proyecto con ID {ProyectoId}.",
            solicitud.ProyectoId);

        return ResultadoT<byte[]>.Exito(pdfBytes);
    }

}