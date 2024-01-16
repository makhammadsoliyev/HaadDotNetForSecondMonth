using Spectre.Console;
using TravelBookingSystem.Models;
using TravelBookingSystem.Services;

namespace TravelBookingSystem.Display;

public class BookingMenu
{
    private readonly BookingService bookingService;

    public BookingMenu(BookingService bookingService)
    {
        this.bookingService = bookingService;
    }

    private async Task Add()
    {
        int packageId = AnsiConsole.Ask<int>("[aqua]PackageId: [/]");
        while (packageId <= 0)
        {
            AnsiConsole.MarkupLine($"[red]Invalid input.[/]");
            packageId = AnsiConsole.Ask<int>("[aqua]PackageId: [/]");
        }
        int customerId = AnsiConsole.Ask<int>("[blue]CustomerId: [/]");
        while (customerId <= 0)
        {
            AnsiConsole.MarkupLine($"[red]Invalid input.[/]");
            customerId = AnsiConsole.Ask<int>("[blue]CustomerId: [/]");
        }
        int numberOfTravelers = AnsiConsole.Ask<int>("[blue]NumberOfTravelers: [/]");
        while (numberOfTravelers <= 0)
        {
            AnsiConsole.MarkupLine($"[red]Invalid input.[/]");
            numberOfTravelers = AnsiConsole.Ask<int>("[blue]NumberOfTravelers: [/]");
        }
        DateOnly travelDate = AnsiConsole.Ask<DateOnly>("[yellow]TravelDate: [/]");

        var booking = new Booking()
        {
            PackageId = packageId,
            CustomerId = customerId,
            TravelDate = travelDate,
            NumberOfTravelers = numberOfTravelers
        };

        try
        {
            var addedBooking = await bookingService.Add(booking);
            AnsiConsole.MarkupLine("[green]Successfully booked...[/]");
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
            var booking = await bookingService.GetById(id);
            var table = new SelectionMenu().DataTable("Booking", booking);
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
        int packageId = AnsiConsole.Ask<int>("[aqua]PackageId: [/]");
        while (packageId <= 0)
        {
            AnsiConsole.MarkupLine($"[red]Invalid input.[/]");
            packageId = AnsiConsole.Ask<int>("[aqua]PackageId: [/]");
        }
        int customerId = AnsiConsole.Ask<int>("[blue]CustomerId: [/]");
        while (customerId <= 0)
        {
            AnsiConsole.MarkupLine($"[red]Invalid input.[/]");
            customerId = AnsiConsole.Ask<int>("[blue]CustomerId: [/]");
        }
        int numberOfTravelers = AnsiConsole.Ask<int>("[blue]NumberOfTravelers: [/]");
        while (numberOfTravelers <= 0)
        {
            AnsiConsole.MarkupLine($"[red]Invalid input.[/]");
            numberOfTravelers = AnsiConsole.Ask<int>("[blue]NumberOfTravelers: [/]");
        }
        DateOnly travelDate = AnsiConsole.Ask<DateOnly>("[yellow]TravelDate: [/]");

        var booking = new Booking()
        {
            PackageId = packageId,
            CustomerId = customerId,
            TravelDate = travelDate,
            NumberOfTravelers = numberOfTravelers
        };

        try
        {
            var updatedBooking = await bookingService.Update(id, booking);
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
            bool isDeleted = await bookingService.Delete(id);
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
        var bookings = await bookingService.GetAll();
        var table = new SelectionMenu().DataTable("Bookings", bookings.ToArray());
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
        var bookings = await bookingService.GetAllByCustomerId(customerId);
        var table = new SelectionMenu().DataTable($"Bookings of CustomerId: {customerId}", bookings.ToArray());
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
