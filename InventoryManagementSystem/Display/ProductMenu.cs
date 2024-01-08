using InventoryManagementSystem.Entities;
using InventoryManagementSystem.Services;
using Spectre.Console;

namespace InventoryManagementSystem.Display;

public class ProductMenu
{
    private readonly ProductService productService;

    public ProductMenu(ProductService productService)
    {
        this.productService = productService;
    }

    private void Add()
    {
        string name = AnsiConsole.Ask<string>("[blue]Name: [/]");
        string description = AnsiConsole.Ask<string>("[cyan2]Description: [/]");
        decimal price = AnsiConsole.Ask<int>("[yellow]Price: [/]");
        while (price <= 0)
        {
            AnsiConsole.MarkupLine($"[red]Invalid input.[/]");
            price = AnsiConsole.Ask<int>("[yellow]Price: [/]");
        }
        int stockQuantity = AnsiConsole.Ask<int>("[blue3]StockQuantity: [/]");
        while (stockQuantity < 0)
        {
            AnsiConsole.MarkupLine($"[red]Invalid input.[/]");
            stockQuantity = AnsiConsole.Ask<int>("[blue3]StockQuantity: [/]");
        }
        int supplierId = AnsiConsole.Ask<int>("[aqua]SupplierId: [/]");
        while (supplierId <= 0)
        {
            AnsiConsole.MarkupLine($"[red]Invalid input.[/]");
            supplierId = AnsiConsole.Ask<int>("[aqua]SupplierId: [/]");
        }

        var product = new Product()
        {
            Name = name,
            Price = price,
            SupplierId = supplierId,
            Description = description,
            StockQuantity = stockQuantity
        };

        try
        {
            var addedProduct = productService.Add(product);
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
            var product = productService.GetById(id);
            var table = new SelectionMenu().DataTable("Product", product);
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
        string name = AnsiConsole.Ask<string>("[blue]Name: [/]");
        string description = AnsiConsole.Ask<string>("[cyan2]Description: [/]");
        decimal price = AnsiConsole.Ask<int>("[yellow]Price: [/]");
        while (price <= 0)
        {
            AnsiConsole.MarkupLine($"[red]Invalid input.[/]");
            price = AnsiConsole.Ask<int>("[yellow]Price: [/]");
        }
        int stockQuantity = AnsiConsole.Ask<int>("[blue3]StockQuantity: [/]");
        while (stockQuantity < 0)
        {
            AnsiConsole.MarkupLine($"[red]Invalid input.[/]");
            stockQuantity = AnsiConsole.Ask<int>("[blue3]StockQuantity: [/]");
        }
        int supplierId = AnsiConsole.Ask<int>("[aqua]SupplierId: [/]");
        while (supplierId <= 0)
        {
            AnsiConsole.MarkupLine($"[red]Invalid input.[/]");
            supplierId = AnsiConsole.Ask<int>("[aqua]SupplierId: [/]");
        }

        var product = new Product()
        {
            Id = id,
            Name = name,
            Price = price,
            SupplierId = supplierId,
            Description = description,
            StockQuantity = stockQuantity
        };

        try
        {
            var updatedProduct = productService.Update(id, product);
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
            bool isDeleted = productService.Delete(id);
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
        var products = productService.GetAll().ToArray();
        var table = new SelectionMenu().DataTable("Products", products);
        AnsiConsole.Write(table);
        AnsiConsole.MarkupLine("[blue]Enter to continue...[/]");
        Console.ReadKey();
    }

    private void GetAllAvailable()
    {
        var products = productService.GetAllAvailable().ToArray();
        var table = new SelectionMenu().DataTable("Available Products", products);
        AnsiConsole.Write(table);
        AnsiConsole.MarkupLine("[blue]Enter to continue...[/]");
        Console.ReadKey();
    }

    private void GetAllSoldOut()
    {
        var products = productService.GetAllSoldOut().ToArray();
        var table = new SelectionMenu().DataTable("Sold Out Products", products);
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
                new string[] { "Add", "GetById", "Update", "Delete", "GetAll", "GetAllAvailable", "GetAllSoldOut", "Back" });

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
                case "GetAllAvailable":
                    GetAllAvailable();
                    break;
                case "GetAllSoldOut":
                    GetAllSoldOut();
                    break;
                case "Back":
                    circle = false;
                    break;
            }
        }
    }
}
