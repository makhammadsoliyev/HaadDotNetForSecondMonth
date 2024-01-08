using InventoryManagementSystem.Entities;
using InventoryManagementSystem.Interfaces;
using InventoryManagementSystem.Services;
using Spectre.Console;

namespace InventoryManagementSystem.Display;

public class SaleMenu
{
    private readonly SaleService saleService;

    public SaleMenu(SaleService saleService)
    {
        this.saleService = saleService;
    }

    private void Add()
    {
        int productId = AnsiConsole.Ask<int>("[aqua]ProductId: [/]");
        while (productId <= 0)
        {
            AnsiConsole.MarkupLine($"[red]Invalid input.[/]");
            productId = AnsiConsole.Ask<int>("[aqua]ProductId: [/]");
        }

        int customerId = AnsiConsole.Ask<int>("[aqua]CustomerId [/]");
        while (customerId <= 0)
        {
            AnsiConsole.MarkupLine($"[red]Invalid input.[/]");
            customerId = AnsiConsole.Ask<int>("[aqua]CustomerId: [/]");
        }

        int quantity = AnsiConsole.Ask<int>("[aqua]Quantity: [/]");
        while (quantity <= 0)
        {
            AnsiConsole.MarkupLine($"[red]Invalid input.[/]");
            quantity = AnsiConsole.Ask<int>("[aqua]Quantity: [/]");
        }

        var sale = new Sale()
        {
            Quantity = quantity,
            ProductId = productId,
            CustomerId = customerId,
            SoldDate = DateTime.Now
        };

        try
        {
            var addedSale = saleService.Add(sale);
            AnsiConsole.MarkupLine("[green]Successfully added...[/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
        }
        Thread.Sleep(1500);
    }

    private void GetById()
    {
        int id = AnsiConsole.Ask<int>("[aqua]Id: [/]");
        while (id <= 0)
        {
            AnsiConsole.MarkupLine($"[red]Invalid input.[/]");
            id = AnsiConsole.Ask<int>("[aqua]Id: [/]");
        }

        try
        {
            var sale = saleService.GetById(id);
            var table = new SelectionMenu().DataTable("Sale", sale);
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

    private void Update()
    {
        int id = AnsiConsole.Ask<int>("[aqua]Id: [/]");
        while (id <= 0)
        {
            AnsiConsole.MarkupLine($"[red]Invalid input.[/]");
            id = AnsiConsole.Ask<int>("[aqua]Id: [/]");
        }

        int productId = AnsiConsole.Ask<int>("[aqua]ProductId: [/]");
        while (productId <= 0)
        {
            AnsiConsole.MarkupLine($"[red]Invalid input.[/]");
            productId = AnsiConsole.Ask<int>("[aqua]ProductId: [/]");
        }

        int customerId = AnsiConsole.Ask<int>("[aqua]CustomerId [/]");
        while (customerId <= 0)
        {
            AnsiConsole.MarkupLine($"[red]Invalid input.[/]");
            customerId = AnsiConsole.Ask<int>("[aqua]CustomerId: [/]");
        }

        int quantity = AnsiConsole.Ask<int>("[aqua]Quantity: [/]");
        while (quantity <= 0)
        {
            AnsiConsole.MarkupLine($"[red]Invalid input.[/]");
            quantity = AnsiConsole.Ask<int>("[aqua]Quantity: [/]");
        }

        var sale = new Sale()
        {
            Id = id,
            Quantity = quantity,
            ProductId = productId,
            CustomerId = customerId,
            SoldDate = DateTime.Now
        };

        try
        {
            var updatedSale = saleService.Update(id, sale);
            AnsiConsole.MarkupLine("[green]Successfully updated...[/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
        }
        Thread.Sleep(1500);
    }

    private void Delete()
    {
        int id = AnsiConsole.Ask<int>("[aqua]Id: [/]");
        while (id <= 0)
        {
            AnsiConsole.MarkupLine($"[red]Invalid input.[/]");
            id = AnsiConsole.Ask<int>("[aqua]Id: [/]");
        }

        try
        {
            bool isDeleted = saleService.Delete(id);
            AnsiConsole.MarkupLine("[green]Successfully deleted...[/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
        }
        Thread.Sleep(1500);
    }

    private void GetAll()
    {
        var sales = saleService.GetAll().ToArray();
        var table = new SelectionMenu().DataTable("Sales", sales);
        AnsiConsole.Write(table);
        AnsiConsole.MarkupLine("[blue]Enter to continue...[/]");
        Console.ReadKey();
    }

    public void Display()
    {
        var circle = true;
        var selectionDisplay = new SelectionMenu();

        while (circle)
        {
            AnsiConsole.Clear();
            var selection = selectionDisplay.ShowSelectionMenu("Choose one of options",
                new string[] { "Add", "GetById", "Update", "Delete", "GetAll", "Back" });

            switch (selection)
            {
                case "Add":
                    Add();
                    break;
                case "GetById":
                    GetById();
                    break;
                case "Update":
                    Update();
                    break;
                case "Delete":
                    Delete();
                    break;
                case "GetAll":
                    GetAll();
                    break;
                case "Back":
                    circle = false;
                    break;
            }
        }
    }
}
