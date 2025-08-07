using Boardly.Aplicacion.DTOs.PDF;
using Boardly.Dominio.Puertos.CasosDeUso.PDF;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Boardly.Infraestructura.Compartido.Adaptadores.PDF;

public class GeneradorPdf : IGeneradorPdf<List<ProyectoDatosPdf>>
{
    public async Task<byte[]> GenerarReporteAsync(List<ProyectoDatosPdf> listaDatos)
    {
        QuestPDF.Settings.License = LicenseType.Community;

        var documento = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(2, Unit.Centimetre);

                page.Header().Height(50).Text("Reporte de Proyectos Finalizados").FontSize(16).Bold().AlignCenter();
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
                    foreach (var datos in listaDatos)
                    {
                        col.Item().Text($"Fecha de generación: {datos.Fecha:dd/MM/yyyy}").Italic().FontSize(10);

                        col.Item().PaddingTop(10).Table(tabla =>
                        {
                            tabla.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                            });

                            tabla.Header(header =>
                            {
                                header.Cell().Text("Nombre").Bold();
                                header.Cell().Text("Descripción").Bold();
                                header.Cell().Text("Fecha inicio").Bold();
                                header.Cell().Text("Fecha fin").Bold();
                            });

                            foreach (var proyecto in datos.ProyectoDto)
                            {
                                tabla.Cell().Text(proyecto.Nombre);
                                tabla.Cell().Text(proyecto.Descripcion ?? "-");
                                tabla.Cell().Text(proyecto.FechaInicio?.ToString("dd/MM/yyyy") ?? "-");
                                tabla.Cell().Text(proyecto.FechaFin?.ToString("dd/MM/yyyy") ?? "-");
                            }
                        });

                        col.Item().PaddingVertical(10).LineHorizontal(0.5f); // Separador entre secciones
                    }
                });
            });
        });

        var pdf = documento.GeneratePdf();
        return await Task.FromResult(pdf);
    }
}
