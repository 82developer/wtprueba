using System;
using Microsoft.AspNetCore.Mvc;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace PdfGenApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PdfController : ControllerBase
    {
        [HttpGet("generate")]
        public IActionResult Generate()
        {
            var pdf = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.DefaultTextStyle(x => x.FontSize(12));

                    page.Header()
                        .AlignCenter()
                        .Text("PDF Generado con QuestPDF")
                        .FontSize(20).Bold();

                    page.Content()
                        .PaddingVertical(1, Unit.Centimetre)
                        .Column(col =>
                        {
                            col.Item().Text($"Fecha: {DateTime.Now:dd/MM/yyyy HH:mm:ss}");
                            col.Item().LineHorizontal(1);
                            col.Item().PaddingTop(10).Text("Hola Mundo desde .NET Core 3.1!");
                        });

                    page.Footer()
                        .AlignCenter()
                        .Text(x =>
                        {
                            x.Span("Pagina ");
                            x.CurrentPageNumber();
                        });
                });
            }).GeneratePdf();

            return File(pdf, "application/pdf", "documento.pdf");
        }
    }
}
