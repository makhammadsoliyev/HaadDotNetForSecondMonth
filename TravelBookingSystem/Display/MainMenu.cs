using Spectre.Console;
using TravelBookingSystem.Services;

namespace TravelBookingSystem.Display;

public class MainMenu
{
    private readonly BookingService bookingService;
    private readonly PaymentService paymentService;
    private readonly CustomerService customerService;
    private readonly TravelPackageService travelPackageService;

    private readonly BookingMenu bookingMenu;
    private readonly PaymentMenu paymentMenu;
    private readonly CustomerMenu customerMenu;
    private readonly TravelPackageMenu travelPackageMenu;

    public MainMenu()
    {
        this.customerService = new CustomerService();
        this.travelPackageService = new TravelPackageService();
        this.bookingService = new BookingService(customerService, travelPackageService);
        this.paymentService = new PaymentService(bookingService, travelPackageService);

        this.bookingMenu = new BookingMenu(bookingService);
        this.paymentMenu = new PaymentMenu(paymentService);
        this.customerMenu = new CustomerMenu(customerService);
        this.travelPackageMenu = new TravelPackageMenu(travelPackageService);
    }

    public async Task Main()
    {
        var circle = true;
        var selectionDisplay = new SelectionMenu();

        while (circle)
        {
            AnsiConsole.Clear();
            var selection = selectionDisplay.ShowSelectionMenu("Choose one of options", new string[] { "Customer", "TravelPackage", "Booking", "Payment", "Exit" });

            switch (selection)
            {
                case "Customer":
                    await customerMenu.Display();
                    break;
                case "TravelPackage":
                    await travelPackageMenu.Display();
                    break;
                case "Booking":
                    await bookingMenu.Display();
                    break;
                case "Payment":
                    await paymentMenu.Display();
                    break;
                case "Exit":
                    circle = false;
                    break;
            }
        }
    }
}
