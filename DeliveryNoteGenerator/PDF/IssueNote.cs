using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace DeliveryNoteGenerator.PDF;

public class IssueNote : IDocument
{
    public DocumentMetadata GetMetadata() => DocumentMetadata.Default;
    public DocumentSettings GetSettings() => DocumentSettings.Default;

    public void Compose(IDocumentContainer container)
    {
        container
            .Page(page =>
            {
                page.Margin(50);
                page.Header().Height(100).Background(Colors.Grey.Darken1);
                page.Content().Background(Colors.Grey.Lighten3);
                page.Footer().Height(50).Background(Colors.Grey.Lighten1);
            });
    }

    // void ComposeHeader(IContainer container)
    // {
    //     container.Row(row =>
    //     {
    //         row.RelativeItem().Column(column => {
    //             column.Item().Text("Delivery note");
    //         });
    //     });
    // }
}