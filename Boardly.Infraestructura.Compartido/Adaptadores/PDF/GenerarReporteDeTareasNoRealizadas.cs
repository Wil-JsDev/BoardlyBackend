using Boardly.Aplicacion.DTOs.Tarea;
using Boardly.Dominio.Puertos.CasosDeUso.PDF;
using Boardly.Dominio.Utilidades;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Boardly.Infraestructura.Compartido.Adaptadores.PDF;
public class GenerarReporteDeTareasNoRealizadas : IGeneradorPdf<IList<TareasNoFinalizadasPdf>>
{
    public async Task<byte[]> GenerarReporteAsync(IList<TareasNoFinalizadasPdf> datos)
    {
        QuestPDF.Settings.License = LicenseType.Community;

        var documento = QuestPDF.Fluent.Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(2, Unit.Centimetre);

                page.Header().Height(50).Text("Reporte de Tareas No Finalizadas").FontSize(16).Bold().AlignCenter();

                page.Footer().Height(30).AlignCenter().Text(text =>
                {
                    text.DefaultTextStyle(x => x.FontSize(10));
                    text.Span("Página ");
                    text.CurrentPageNumber();
                    text.Span(" de ");
                    text.TotalPages();
                });

                page.Content().Column(col =>
                {
                    foreach (var proyecto in datos)
                    {
                        col.Item().Text($"Fecha de generación: {proyecto.FechaGeneracion:dd/MM/yyyy}");

                        col.Item().PaddingBottom(10).Table(tabla =>
                        {
                            tabla.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(); // Nombre
                                columns.RelativeColumn(); // Fecha vencimiento
                                columns.RelativeColumn(); // Estado
                            });

                            tabla.Header(header =>
                            {
                                header.Cell().Text("Nombre").Bold();
                                header.Cell().Text("Fecha Vencimiento").Bold();
                                header.Cell().Text("Estado").Bold();
                            });

                            foreach (var tarea in proyecto.Tareas)
                            {
                                tabla.Cell().Text(tarea.Nombre);
                                tabla.Cell().Text(tarea.FechaVencimiento.ToString("dd/MM/yyyy"));
                                tabla.Cell().Text(tarea.Estado);
                            }
                        });
                    }
                });
            });
        });

        var pdf = documento.GeneratePdf();
        return await Task.FromResult(pdf);
    }
}
