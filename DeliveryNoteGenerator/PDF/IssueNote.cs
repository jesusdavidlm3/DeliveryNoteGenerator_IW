using System.Collections.ObjectModel;
using DeliveryNoteGenerator.Models;
using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace DeliveryNoteGenerator.PDF;

public class IssueNote : IDocument
{
    public DocumentMetadata GetMetadata() => DocumentMetadata.Default;
    public DocumentSettings GetSettings() => DocumentSettings.Default;
    public User SelectedUser { get; }
    public ObservableCollection<Asset> SelectedAssets { get; }
    public User LoggedUser { get; set; }
    public DateTime IssueDate { get; set; }
    
    public IssueNote(User selectedUser, ObservableCollection<Asset> selectedAssets, User loggedUser, DateTime issueDate)
    {
        SelectedUser = selectedUser;
        SelectedAssets = selectedAssets;
        LoggedUser = loggedUser;
        IssueDate = issueDate;
    }
    
    public void Compose(IDocumentContainer container)
    {
        container
            .Page(page =>
            {
                page.Margin(50);
                page.Header().Height(100).Element(ComposeHeader);
                page.Content().Element(ComposeBody);
            });
    }

    void ComposeHeader(IContainer container)
    {
        container.Row(row =>
        {
            row.RelativeItem().Column(column => {
                column.Item().Image("logo.png").FitArea();
            });
            row.RelativeItem().AlignMiddle().Column(column =>
            {
                column.Item().Text("Nota de entrega").FontSize(22).AlignCenter();
            });
            row.RelativeItem().Column(column =>
            {
                column.Item().Text($"Fecha: {IssueDate.Day}/{IssueDate.Month}/{IssueDate.Year}").AlignEnd().FontSize(9);
            });
        });
    }

    void ComposeBody(IContainer container)
    {
        
        container.PaddingTop(30).Column(column =>
        {
            column.Item().Row(row =>
            {
                row.RelativeItem().PaddingBottom(20).Text($"Yo, {SelectedUser.name}, venezolano, mayor de edad, cedula de identidad _______________ recibo y me responsabilizo por el siguiente equipo:").Justify().FontSize(9);
            });
        
            column.Item().Row(row =>
            {
                row.RelativeItem().AlignCenter().PaddingBottom(20).Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.ConstantColumn(110);
                        columns.ConstantColumn(250);
                        columns.ConstantColumn(90);
                    });
                
                    table.Header(header =>
                    {
                        header.Cell().Border(1).Padding(5).Text("Tag").AlignCenter().FontSize(9);
                        header.Cell().Border(1).Padding(5).Text("Descripcion").AlignCenter().FontSize(9);
                        header.Cell().Border(1).Padding(5).Text("Cantidad").AlignCenter().FontSize(9);
                    });

                    foreach (var asset in SelectedAssets)
                    {
                        table.Cell().Border(1).Padding(7).Text(asset.asset_tag).AlignCenter().FontSize(9);
                        table.Cell().Border(1).Padding(7).Text(asset.name).FontSize(9);
                        table.Cell().Border(1).Padding(7).Text($"{asset.Quantity}").AlignCenter().FontSize(9);
                    }
                });
            }); 
            
            column.Item().AlignLeft().Padding(30).Row(row =>
            {
                row.RelativeItem().Column(column =>
                {
                    column.Item().Text("Recibido por:").FontSize(9);
                    column.Item().Text(SelectedUser.name).FontSize(9);
                    column.Item().PaddingBottom(10).Text("Firma:").FontSize(9);
                    column.Item().PaddingBottom(20).Text("___________________").FontSize(9);
                    column.Item().Text("Entregado por:").FontSize(9);
                    column.Item().Text(SelectedUser.name).FontSize(9);
                    column.Item().PaddingBottom(10).Text("Firma:").FontSize(9);
                    column.Item().Text("___________________").FontSize(9);
                });
                
                row.RelativeItem().AlignRight().Column(column =>
                {
                    column.Item().Text("Entregado por:").FontSize(9);
                    column.Item().Text(LoggedUser.name).FontSize(9);
                    column.Item().PaddingBottom(10).Text("Firma:").FontSize(9);
                    column.Item().PaddingBottom(20).Text("___________________").FontSize(9);
                    column.Item().Text("Recibido por:").FontSize(9);
                    column.Item().Text(LoggedUser.name).FontSize(9);
                    column.Item().PaddingBottom(10).Text("Firma:").FontSize(9);
                    column.Item().Text("___________________").FontSize(9);
                });
            });
        });
    }
}