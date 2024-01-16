using Spectre.Console;
using System.Text.RegularExpressions;
using TravelBookingSystem.Models;
using TravelBookingSystem.Services;

namespace TravelBookingSystem.Display;

public class CustomerMenu
{
    private readonly CustomerService customerService;

    public CustomerMenu(CustomerService customerService)
    {
        this.customerService = customerService;
    }

    private async Task Add()
    {
        string firstName = AnsiConsole.Ask<string>("[blue]FirstName: [/]");
        string lastName = AnsiConsole.Ask<string>("[cyan2]LastName: [/]");
        string phone = AnsiConsole.Ask<string>("[cyan1]Phone(+998XXxxxxxxx): [/]");
        while (!Regex.IsMatch(phone, @"^\+998\d{9}$"))
        {
            AnsiConsole.MarkupLine("[red]Invalid input.[/]");
            phone = AnsiConsole.Ask<string>("[cyan1]Phone(+998XXxxxxxxx): [/]");
        }

        var customer = new Customer()
        {
            Phone = phone,
            LastName = lastName,
            FirstName = firstName
        };

        try
        {
            var addedCustomer = await customerService.Add(customer);
            AnsiConsole.MarkupLine("[green]Successfully added...[/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
        }
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
            var customer = await customerService.GetById(id);
            var table = new SelectionMenu().DataTable("Customer", customer);
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
        string firstName = AnsiConsole.Ask<string>("[blue]FirstName: [/]");
        string lastName = AnsiConsole.Ask<string>("[cyan2]LastName: [/]");
        string phone = AnsiConsole.Ask<string>("[cyan1]Phone(+998XXxxxxxxx): [/]");
        while (!Regex.IsMatch(phone, @"^\+998\d{9}$"))
        {
            AnsiConsole.MarkupLine("[red]Invalid input.[/]");
            phone = AnsiConsole.Ask<string>("[cyan1]Phone(+998XXxxxxxxx): [/]");
        }

        var customer = new Customer()
        {
            Id = id,
            Phone = phone,
            LastName = lastName,
            FirstName = firstName
        };

        try
        {
            var updatedCustomer = await customerService.Update(id, customer);
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
            bool isDeleted = await customerService.Delete(id);
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
        var customers = await customerService.GetAll();
        var table = new SelectionMenu().DataTable("Customers", customers.ToArray());
        AnsiConsole.Write(table);
        AnsiConsole.MarkupLine("[blue]Enter to continue...[/]");
        Console.ReadKey();
    }

    private async Task SearchByName()
    {
        string name = AnsiConsole.Ask<string>("[blue]FullName(first, last): [/]").Trim();
        var customers = await customerService.SearchByName(name);
        var table = new SelectionMenu().DataTable(name, customers.ToArray());
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
                new string[] { "Add", "GetById", "Update", "Delete", "GetAll", "SearchByName", "Back" });

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
                case "SearchByName":
                    await SearchByName();
                    break;
                case "Back":
                    circle = false;
                    break;
            }
        }
    }
}
