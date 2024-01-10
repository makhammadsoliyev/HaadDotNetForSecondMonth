using CompressAndExtract.Services;
using Spectre.Console;

namespace CompressAndExtract.Display;

public class MainMenu
{
    private readonly CompressionService compressionService;

    public MainMenu()
    {
        this.compressionService = new CompressionService();
    }

    private void CompressFolder()
    {
        string sourceFolderPath = AnsiConsole.Ask<string>("[blue]sourceFolderPath: [/]").Trim();
        string zipFilePath = AnsiConsole.Ask<string>("[cyan2]zipFilePath: [/]").Trim();

        try
        {
            compressionService.CompressFolder(sourceFolderPath, zipFilePath);
            AnsiConsole.MarkupLine("[green]Successfully zipped...[/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
        }
        AnsiConsole.MarkupLine("[blue]Enter to continue...[/]");
        Console.ReadKey();
    }

    private void DecompressFolder()
    {
        string zipFilePath = AnsiConsole.Ask<string>("[cyan2]zipFilePath: [/]").Trim();
        string extractToFolderPath = AnsiConsole.Ask<string>("[blue]extractToFolderPath: [/]").Trim();

        try
        {
            compressionService.DecompressFolder(zipFilePath, extractToFolderPath);
            AnsiConsole.MarkupLine("[green]Successfully unzipped...[/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
        }
        AnsiConsole.MarkupLine("[blue]Enter to continue...[/]");
        Console.ReadKey();
    }

    private void CompressFile()
    {
        string sourceFilePath = AnsiConsole.Ask<string>("[blue]sourceFilePath: [/]").Trim();
        string zipFilePath = AnsiConsole.Ask<string>("[cyan2]zipFilePath: [/]").Trim();

        try
        {
            compressionService.CompressFile(sourceFilePath, zipFilePath);
            AnsiConsole.MarkupLine("[green]Successfully zipped...[/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
        }
        AnsiConsole.MarkupLine("[blue]Enter to continue...[/]");
        Console.ReadKey();
    }

    private void DecompressFile()
    {
        string zipFilePath = AnsiConsole.Ask<string>("[cyan2]zipFilePath: [/]").Trim();
        string extractToFolderPath = AnsiConsole.Ask<string>("[blue]extractToFolderPath: [/]").Trim();

        try
        {
            compressionService.DecompressFile(zipFilePath, extractToFolderPath);
            AnsiConsole.MarkupLine("[green]Successfully unzipped...[/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
        }
        AnsiConsole.MarkupLine("[blue]Enter to continue...[/]");
        Console.ReadKey();
    }

    private string ShowSelectionMenu(string title, string[] options)
    {
        var selection = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title(title)
                .PageSize(5) // Number of items visible at once
                .AddChoices(options)
                .HighlightStyle(new Style(foreground: Color.Cyan1, background: Color.Blue))
        );

        return selection;
    }

    public void Display()
    {
        var circle = true;

        while (circle)
        {
            AnsiConsole.Clear();
            var selection = ShowSelectionMenu("Choose one of options", new string[] { "CompressFolder", "DecompressFolder", "CompressFile", "DecompressFile", "Exit" });

            switch (selection)
            {
                case "CompressFolder":
                    CompressFolder();
                    break;
                case "DecompressFolder":
                    DecompressFolder();
                    break;
                case "CompressFile":
                    CompressFile();
                    break;
                case "DecompressFile":
                    DecompressFile();
                    break;
                case "Exit":
                    circle = false;
                    break;
            }
        }
    }
}
