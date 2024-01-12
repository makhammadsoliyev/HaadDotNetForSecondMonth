using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using Spectre.Console;


while (true)
{
    AnsiConsole.Clear();
    string pdfFilePath = AnsiConsole.Ask<string>("[blue]pdfFilePath: [/]").Trim();
    string outputTxtFilePath = AnsiConsole.Ask<string>("[cyan2]outputTxtFilePath: [/]").Trim();
    try
    {
        PDFtoTextConverter(pdfFilePath, outputTxtFilePath);
        AnsiConsole.MarkupLine("[green]Successfully converted...[/]");
        break;
    }
    catch (Exception ex)
    {
        AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
        Thread.Sleep(2000);
    }
}

static void PDFtoTextConverter(string pdfFilePath, string outputTxtFilePath)
{
    PdfReader reader = new PdfReader(pdfFilePath);
    TextWriter writer = new StreamWriter(outputTxtFilePath);

    for (int i = 1; i <= reader.NumberOfPages; i++)
        writer.Write(PdfTextExtractor.GetTextFromPage(reader, i, new SimpleTextExtractionStrategy()));

    writer.Close();
    reader.Close();
}
