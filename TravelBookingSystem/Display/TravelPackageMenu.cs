using Spectre.Console;
using TravelBookingSystem.Models;
using TravelBookingSystem.Services;

namespace TravelBookingSystem.Display;

public class TravelPackageMenu
{
    private readonly TravelPackageService travelPackageService;

    public TravelPackageMenu(TravelPackageService travelPackageService)
    {
        this.travelPackageService = travelPackageService;
    }

    private async Task Add()
    {
        string name = AnsiConsole.Ask<string>("[blue]Name: [/]");
        string destination = AnsiConsole.Ask<string>("[cyan2]Destination: [/]");
        int duration = AnsiConsole.Ask<int>("[aqua]Duration(In days): [/]");
        decimal price = AnsiConsole.Ask<decimal>("[cyan1]Price: [/]");
        int spots = AnsiConsole.Ask<int>("[cyan3]Number of spots: [/]");
        string itinerary = AnsiConsole.Ask<string>("[yellow]Itinerary: [/]");

        var package = new TravelPackage()
        {
            Name = name,
            Price = price,
            Spots = spots,
            Duration = duration,
            Itinerary = itinerary,
            Destination = destination
        };

        var addedPackage = await travelPackageService.Add(package);
        AnsiConsole.MarkupLine("[green]Successfully added...[/]");
        Thread.Sleep(1500);
    }

    private async Task GetById()
    {
        int id = AnsiConsole.Ask<int>("[aqua]Id: [/]");
        while (id <= 0)
        {
            AnsiConsole.MarkupLine($"[red]Invalid input.[/]");
            id = AnsiConsole.Ask<int>("[aqua]Id: [/]");
        }

        try
        {
            var package = await travelPackageService.GetById(id);
            var table = new SelectionMenu().DataTable("Travel Package", package);
            AnsiConsole.Write(table);
            AnsiConsole.MarkupLine("[blue]Enter to continue...[/]");
            Console.ReadKey();
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
            Thread.Sleep(1500);
        }
    }

    private async Task Update()
    {
        int id = AnsiConsole.Ask<int>("[aqua]Id: [/]");
        while (id <= 0)
        {
            AnsiConsole.MarkupLine($"[red]Invalid input.[/]");
            id = AnsiConsole.Ask<int>("[aqua]Id: [/]");
        }
        string name = AnsiConsole.Ask<string>("[blue]Name: [/]");
        string destination = AnsiConsole.Ask<string>("[cyan2]Destination: [/]");
        int duration = AnsiConsole.Ask<int>("[aqua]Duration(In days): [/]");
        decimal price = AnsiConsole.Ask<decimal>("[cyan1]Price: [/]");
        int spots = AnsiConsole.Ask<int>("[cyan3]Number of spots: [/]");
        string itinerary = AnsiConsole.Ask<string>("[yellow]Itinerary: [/]");

        var package = new TravelPackage()
        {
            Name = name,
            Price = price,
            Spots = spots,
            Duration = duration,
            Itinerary = itinerary,
            Destination = destination
        };

        try
        {
            var updatedPackage = await travelPackageService.Update(id, package);
            AnsiConsole.MarkupLine("[green]Successfully updated...[/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
        }
        Thread.Sleep(1500);
    }

    private async Task Delete()
    {
        int id = AnsiConsole.Ask<int>("[aqua]Id: [/]");
        while (id <= 0)
        {
            AnsiConsole.MarkupLine($"[red]Invalid input.[/]");
            id = AnsiConsole.Ask<int>("[aqua]Id: [/]");
        }

        try
        {
            bool isDeleted = await travelPackageService.Delete(id);
            AnsiConsole.MarkupLine("[green]Successfully deleted...[/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
        }
        Thread.Sleep(1500);
    }

    private async Task GetAll()
    {
        var packages = await travelPackageService.GetAll();
        var table = new SelectionMenu().DataTable("Travel Package", packages.ToArray());
        AnsiConsole.Write(table);
        AnsiConsole.MarkupLine("[blue]Enter to continue...[/]");
        Console.ReadKey();
    }

    private async Task SearchByPackageName()
    {
        string name = AnsiConsole.Ask<string>("[blue]Name: [/]");
        var packages = await travelPackageService.SearchByPackageName(name);
        var table = new SelectionMenu().DataTable(name, packages.ToArray());
        AnsiConsole.Write(table);
        AnsiConsole.MarkupLine("[blue]Enter to continue...[/]");
        Console.ReadKey();
    }

    public async Task Display()
    {
        var circle = true;
        var selectionDisplay = new SelectionMenu();

        while (circle)
        {
            AnsiConsole.Clear();
            var selection = selectionDisplay.ShowSelectionMenu("Choose one of options",
                new string[] { "Add", "GetById", "Update", "Delete", "GetAll", "SearchByPackageName", "Back" });

            switch (selection)
            {
                case "Add":
                    await Add();
                    break;
                case "GetById":
                    await GetById();
                    break;
                case "Update":
                    await Update();
                    break;
                case "Delete":
                    await Delete();
                    break;
                case "GetAll":
                    await GetAll();
                    break;
                case "SearchByPackageName":
                    await SearchByPackageName();
                    break;
                case "Back":
                    circle = false;
                    break;
            }
        }
    }
}
