using Bingo.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Bingo.Helper
{
    public class ManagerPDF
    {
        public static byte[] GenerartePDF(List<CardDto> cards)
        {
            QuestPDF.Settings.License = LicenseType.Community;

            var pdf = Document.Create(container =>
            {
                int cardsPerPage = 4;
                int cardWidth = 235;
                int cardHeight = 320;
                int cardMargin = 5;
                int cellSize = 40;

                for (int i = 0; i < cards.Count; i += cardsPerPage)
                {
                    container.Page(page =>
                    {
                        page.Size(PageSizes.A4);
                        page.PageColor(Colors.White);
                        page.Margin(2, Unit.Centimetre);

                        page.Content()
                            .Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.ConstantColumn(cardWidth + cardMargin);
                                    columns.ConstantColumn(cardWidth + cardMargin);
                                });

                                for (int j = 0; j < cardsPerPage; j++)
                                {
                                    if (i + j >= cards.Count)
                                        break;

                                    var card = cards[i + j];

                                    table.Cell().Height(cardHeight).Width(cardWidth)
                                        .Padding(cardMargin)
                                        .Border(1)
                                        .Background(Colors.Green.Accent1)
                                        .Column(cardColumn =>
                                        {
                                            // HEADER
                                            cardColumn.Item().PaddingBottom(5).Column(header =>
                                            {
                                                header.Item().Text(card.City).FontSize(14).Bold().AlignCenter();
                                            });

                                            cardColumn.Item().Row(row =>
                                            {
                                                cardColumn.Item().BorderBottom(1).Row(row =>
                                                {
                                                    string[] columns = { "B", "I", "N", "G", "O" };

                                                    foreach (var bingo in columns)
                                                    {
                                                        row.RelativeItem().Text(bingo).Bold().AlignCenter().FontSize(20);
                                                    }
                                                });
                                            });

                                            // BODY
                                            cardColumn.Item().AlignCenter().PaddingVertical(5).Table(bodyTable =>
                                            {
                                                bodyTable.ColumnsDefinition(columns =>
                                                {
                                                    // Define las columnas con un tamaño constante
                                                    columns.ConstantColumn(cellSize);
                                                    columns.ConstantColumn(cellSize);
                                                    columns.ConstantColumn(cellSize);
                                                    columns.ConstantColumn(cellSize);
                                                    columns.ConstantColumn(cellSize);
                                                });

                                                // Crear la tabla de datos
                                                for (int row = 0; row < 5; row++)
                                                {
                                                    bodyTable
                                                    .Cell()
                                                    .Width(cellSize)
                                                    .Height(cellSize)
                                                    .Padding(2)
                                                    .Border(1)
                                                    .AlignMiddle()
                                                    .AlignCenter()
                                                    .Text(card.Data[row * 5 + 0])
                                                    .FontSize(20);

                                                    bodyTable
                                                    .Cell()
                                                    .Width(cellSize)
                                                    .Height(cellSize)
                                                    .Padding(2)
                                                    .Border(1)
                                                    .AlignMiddle()
                                                    .AlignCenter()
                                                    .Text(card.Data[row * 5 + 1])
                                                    .FontSize(20); ;

                                                    bodyTable
                                                    .Cell()
                                                    .Width(cellSize)
                                                    .Height(cellSize)
                                                    .Padding(2)
                                                    .Border(1)
                                                    .AlignMiddle()
                                                    .AlignCenter()
                                                    .Text(card.Data[row * 5 + 2])
                                                    .FontSize(20); ;

                                                    bodyTable
                                                    .Cell()
                                                    .Width(cellSize)
                                                    .Height(cellSize)
                                                    .Padding(2)
                                                    .Border(1)
                                                    .AlignMiddle()
                                                    .AlignCenter()
                                                    .Text(card.Data[row * 5 + 3])
                                                    .FontSize(20); ;

                                                    bodyTable
                                                    .Cell()
                                                    .Width(cellSize)
                                                    .Height(cellSize)
                                                    .Padding(2)
                                                    .Border(1)
                                                    .AlignMiddle()
                                                    .AlignCenter()
                                                    .Text(card.Data[row * 5 + 4])
                                                    .FontSize(20); ;
                                                }
                                            });

                                            // FOOTER
                                            cardColumn.Item().BorderTop(1).PaddingVertical(5).AlignCenter()
                                                .Column(footer =>
                                                {
                                                    footer.Item().AlignCenter().Text(card.Developer).FontSize(8);
                                                    footer.Item().AlignCenter().Text($"Telf: {card.Phone}").FontSize(8);
                                                });
                                        });
                                }
                            });
                    });
                }
            }).GeneratePdf();

            return pdf;
        }
    }
}