using System;
using System.IO;
using System.Linq;
using Dominio;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace GestionComercialWeb.Services
{

    public static class FacturaPdfGenerador
    {
        public static byte[] Generar(Venta venta)
        {
            
            QuestPDF.Settings.License = LicenseType.Community;

            var documento = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(40);
                    page.DefaultTextStyle(x => x.FontSize(11).FontFamily("Courier New"));

                    page.Header().Column(col =>
                    {
                        col.Item().AlignCenter().Text("VINOTECA").FontSize(20).Bold();
                        col.Item().AlignCenter().Text($"Factura N° {venta.NumeroFactura}");
                        col.Item().PaddingTop(8).LineHorizontal(1);
                    });

                    page.Content().PaddingVertical(15).Column(col =>
                    {
                        col.Item().Text($"Fecha: {venta.FechaVenta:dd/MM/yyyy}");
                        col.Item().PaddingBottom(10).Text($"Cliente: {venta.Cliente.Nombre} {venta.Cliente.Apellido}");

                        col.Item().Table(tabla =>
                        {
                            tabla.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(3); // Producto
                                columns.RelativeColumn(1); // Cantidad
                                columns.RelativeColumn(1); // Precio
                                columns.RelativeColumn(1); // Subtotal
                            });

                            tabla.Header(header =>
                            {
                                header.Cell().Text("Producto").Bold();
                                header.Cell().AlignRight().Text("Cant.").Bold();
                                header.Cell().AlignRight().Text("Precio").Bold();
                                header.Cell().AlignRight().Text("Subtotal").Bold();
                                header.Cell().ColumnSpan(4).PaddingTop(4).LineHorizontal(1);
                            });

                            foreach (var item in venta.Detalles)
                            {
                                tabla.Cell().Text(item.Producto.NombreProducto);
                                tabla.Cell().AlignRight().Text(item.Cantidad.ToString());
                                tabla.Cell().AlignRight().Text($"${item.PrecioUnitario:N2}");
                                tabla.Cell().AlignRight().Text($"${item.Subtotal:N2}");
                            }
                        });

                        col.Item().PaddingTop(10).LineHorizontal(1);

                        col.Item().AlignRight().PaddingTop(8).Text($"TOTAL: ${venta.Total:N2}")
                            .FontSize(16).Bold();
                    });

                    page.Footer().AlignCenter().Text("Gracias por su compra")
                        .FontSize(9).FontColor(Colors.Grey.Medium);
                });
            });

            using (var stream = new MemoryStream())
            {
                documento.GeneratePdf(stream);
                return stream.ToArray();
            }
        }
    }
}
