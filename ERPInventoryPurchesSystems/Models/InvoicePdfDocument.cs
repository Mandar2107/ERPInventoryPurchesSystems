using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using ERPInventoryPurchesSystems.Models.PR;
using System;
using System.Collections.Generic;

public class InvoicePdfDocument : IDocument
{
    private readonly Invoice _invoice;

    public InvoicePdfDocument(Invoice invoice)
    {
        _invoice = invoice ?? throw new ArgumentNullException(nameof(invoice));
        _invoice.Items ??= new List<InvoiceItem>();
    }

    public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

    public void Compose(IDocumentContainer container)
    {
        container.Page(page =>
        {
            page.Margin(30);
            page.Size(PageSizes.A4);
            page.PageColor(Colors.White);
            page.DefaultTextStyle(x => x.FontSize(12));

            page.Header().Text("INVOICE").FontSize(24).Bold().AlignCenter();

            page.Content().Column(col =>
            {
                col.Spacing(10);

                col.Item().Text($"Invoice #: {_invoice.InvoiceNumber ?? "N/A"}").Bold();
                col.Item().Text($"Date: {_invoice.InvoiceDate:dd-MM-yyyy}");
                col.Item().Text($"Vendor: {_invoice.Vendor?.VendorName ?? "N/A"}");
                col.Item().Text($"Department: {_invoice.Department?.DepartmentName ?? "N/A"}");
                col.Item().Text($"Total Amount: ₹{_invoice.TotalInvoiceAmount:N2}");

                if (_invoice.Items.Count > 0)
                {
                    col.Item().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn(); // Item
                            columns.ConstantColumn(50); // PO
                            columns.ConstantColumn(50); // GRN
                            columns.ConstantColumn(50); // Inv
                            columns.ConstantColumn(60); // Price
                            columns.ConstantColumn(60); // Total
                            columns.RelativeColumn(); // Status
                        });

                        table.Header(header =>
                        {
                            header.Cell().Element(CellStyle).Text("Item").Bold();
                            header.Cell().Element(CellStyle).Text("PO").Bold();
                            header.Cell().Element(CellStyle).Text("GRN").Bold();
                            header.Cell().Element(CellStyle).Text("Inv").Bold();
                            header.Cell().Element(CellStyle).Text("Price").Bold();
                            header.Cell().Element(CellStyle).Text("Total").Bold();
                            header.Cell().Element(CellStyle).Text("Status").Bold();
                        });

                        foreach (var item in _invoice.Items)
                        {
                            table.Cell().Element(CellStyle).Text(item.Item?.ItemName ?? "N/A");
                            table.Cell().Element(CellStyle).Text(item.POQuantity.ToString());
                            table.Cell().Element(CellStyle).Text(item.GRNQuantity.ToString());
                            table.Cell().Element(CellStyle).Text(item.InvoicedQuantity.ToString());
                            table.Cell().Element(CellStyle).Text($"₹{item.UnitPrice:N2}");
                            table.Cell().Element(CellStyle).Text($"₹{item.TotalAmount:N2}");
                            table.Cell().Element(CellStyle).Text(item.MatchStatus ?? "N/A");
                        }

                        IContainer CellStyle(IContainer container) =>
                            container.Border(1).Padding(5).AlignCenter().AlignMiddle();
                    });
                }
                else
                {
                    col.Item().Text("No items available for this invoice.").Italic();
                }

                col.Item().Text($"Payment Method: {_invoice.PaymentMethod ?? "N/A"}");
                col.Item().Text($"Payment Status: {_invoice.PaymentStatus ?? "N/A"}");
                col.Item().Text($"Payment Date: {_invoice.PaymentDate.ToString("dd-MM-yyyy") ?? "N/A"}");
                col.Item().Text($"Processed By: {_invoice.ProcessedBy?.FullName ?? "N/A"}");
            });

            page.Footer().AlignCenter().Text("Thank you for your business!").FontSize(10).Italic();
        });
    }
}
