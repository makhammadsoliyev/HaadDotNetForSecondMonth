using Spectre.Console;
using TravelBookingSystem.Models;

namespace TravelBookingSystem.Display;

public class SelectionMenu
{
    public Table DataTable(string title, params Payment[] payments)
    {
        var table = new Table();

        table.Title(title.ToUpper())
            .BorderColor(Color.Blue)
            .AsciiBorder();

        table.AddColumn("ID");
        table.AddColumn("BookingId");
        table.AddColumn("Status");
        table.AddColumn("TotalAmount");

        foreach (var payment in payments)
            table.AddRow(payment.Id.ToString(), payment.BookingId.ToString(), payment.Status.ToString(), payment.TotalAmount.ToString());

        return table;
    }

    public Table DataTable(string title, params Booking[] bookings)
    {
        var table = new Table();

        table.Title(title.ToUpper())
            .BorderColor(Color.Blue)
            .AsciiBorder();

        table.AddColumn("ID");
        table.AddColumn("PackageId");
        table.AddColumn("CustomerId");
        table.AddColumn("NumberOfTravelers");
        table.AddColumn("TravelDate");

        foreach (var booking in bookings)
            table.AddRow(booking.Id.ToString(), booking.PackageId.ToString(), booking.CustomerId.ToString(), booking.NumberOfTravelers.ToString(), booking.TravelDate.ToString());

        return table;
    }

    public Table DataTable(string title, params TravelPackage[] travelPackages)
    {
        var table = new Table();

        table.Title(title.ToUpper())
            .BorderColor(Color.Blue)
            .AsciiBorder();

        table.AddColumn("ID");
        table.AddColumn("Name");
        table.AddColumn("Destination");
        table.AddColumn("Duration");
        table.AddColumn("Price");
        table.AddColumn("Number of Spots");
        table.AddColumn("Itinerary");

        foreach (var package in travelPackages)
            table.AddRow(package.Id.ToString(), package.Name, package.Destination, package.Duration.ToString(), package.Price.ToString(), package.Spots.ToString(), package.Itinerary);

        return table;
    }

    public Table DataTable(string title, params Customer[] customers)
    {
        var table = new Table();

        table.Title(title.ToUpper())
            .BorderColor(Color.Blue)
            .AsciiBorder();

        table.AddColumn("ID");
        table.AddColumn("FirstName");
        table.AddColumn("LastName");
        table.AddColumn("Phone");

        foreach (var customer in customers)
            table.AddRow(customer.Id.ToString(), customer.FirstName, customer.LastName, customer.Phone);

        return table;
    }

    public string ShowSelectionMenu(string title, string[] options)
    {
        var selection = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title(title)
                .PageSize(5) // Number of items visible at once
                .AddChoices(options)
                .HighlightStyle(new Style(foreground: Color.Cyan2, background: Color.Blue))
        );

        return selection;
    }
}
