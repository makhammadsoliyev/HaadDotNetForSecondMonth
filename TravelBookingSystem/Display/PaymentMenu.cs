using Spectre.Console;
using TravelBookingSystem.Models;
using TravelBookingSystem.Services;

namespace TravelBookingSystem.Display;

public class PaymentMenu
{
    private readonly PaymentService paymentService;

    public PaymentMenu(PaymentService paymentService)
    {
        this.paymentService = paymentService;
    }

    private async Task Add()
    {
        int bookingId = AnsiConsole.Ask<int>("[aqua]BookingId: [/]");
        while (bookingId <= 0)
        {
            AnsiConsole.MarkupLine($"[red]Invalid input.[/]");
            bookingId = AnsiConsole.Ask<int>("[aqua]BookingId: [/]");
        }
        decimal amount = AnsiConsole.Ask<decimal>("[yellow]TotalAmount: [/]");

        var payment = new Payment()
        {
            BookingId = bookingId,
            TotalAmount = amount,
        };

        try
        {
            var addedPayment = await paymentService.Add(payment);
            AnsiConsole.MarkupLine("[green]Successfully paid...[/]");
            Thread.Sleep(1500);
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
            AnsiConsole.MarkupLine("[blue]Enter to continue...[/]");
            Console.ReadKey();
        }
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
            var payment = await paymentService.GetById(id);
            var table = new SelectionMenu().DataTable("Payment", payment);
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
        int bookingId = AnsiConsole.Ask<int>("[aqua]BookingId: [/]");
        while (bookingId <= 0)
        {
            AnsiConsole.MarkupLine($"[red]Invalid input.[/]");
            bookingId = AnsiConsole.Ask<int>("[aqua]BookingId: [/]");
        }
        decimal amount = AnsiConsole.Ask<decimal>("[yellow]TotalAmount: [/]");

        var payment = new Payment()
        {
            Id = id,
            TotalAmount = amount,
            BookingId = bookingId
        };

        try
        {
            var updatedPayment = await paymentService.Update(id, payment);
            AnsiConsole.MarkupLine("[green]Successfully updated...[/]");
            Thread.Sleep(1500);
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
            AnsiConsole.MarkupLine("[blue]Enter to continue...[/]");
            Console.ReadKey();
        }
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
            bool isDeleted = await paymentService.Delete(id);
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
        var payments = await paymentService.GetAll();
        var table = new SelectionMenu().DataTable("Payments", payments.ToArray());
        AnsiConsole.Write(table);
        AnsiConsole.MarkupLine("[blue]Enter to continue...[/]");
        Console.ReadKey();
    }


    private async Task GetAllByCustomerId()
    {
        int customerId = AnsiConsole.Ask<int>("[aqua]CustomerId: [/]");
        while (customerId <= 0)
        {
            AnsiConsole.MarkupLine($"[red]Invalid input.[/]");
            customerId = AnsiConsole.Ask<int>("[aqua]CustomerId: [/]");
        }

        var payments = await paymentService.GetAllByCustomerId(customerId);
        var table = new SelectionMenu().DataTable($"Payments of CustomerId: {customerId}", payments.ToArray());
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
                new string[] { "Add", "GetById", "Update", "Delete", "GetAll", "GetAllByCustomerId", "Back" });

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
                case "GetAllByCustomerId":
                    await GetAllByCustomerId();
                    break;
                case "Back":
                    circle = false;
                    break;
            }
        }
    }
}
