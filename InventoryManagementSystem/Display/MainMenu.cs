using InventoryManagementSystem.Services;
using Spectre.Console;

namespace InventoryManagementSystem.Display;

public class MainMenu
{
    private readonly SaleService saleService;
    private readonly ProductService productService;
    private readonly SupplierService supplierService;
    private readonly CustomerService customerService;

    private readonly SaleMenu saleMenu;
    private readonly ProductMenu productMenu;
    private readonly SupplierMenu supplierMenu;
    private readonly CustomerMenu customerMenu;

    public MainMenu()
    {
        this.supplierService = new SupplierService();
        this.customerService = new CustomerService();
        this.productService = new ProductService(supplierService);
        this.saleService = new SaleService(customerService, productService);

        this.saleMenu = new SaleMenu(saleService);
        this.productMenu = new ProductMenu(productService);
        this.supplierMenu = new SupplierMenu(supplierService);
        this.customerMenu = new CustomerMenu(customerService);
    }

    public void Main()
    {
        var circle = true;
        var selectionDisplay = new SelectionMenu();

        while (circle)
        {
            AnsiConsole.Clear();
            var selection = selectionDisplay.ShowSelectionMenu("Choose one of options", new string[] { "Customer", "Supplier", "Product",  "Sale", "Exit" });

            switch (selection)
            {
                case "Customer":
                    customerMenu.Display();
                    break;
                case "Supplier":
                    supplierMenu.Display();
                    break;
                case "Product":
                    productMenu.Display();
                    break;
                case "Sale":
                    saleMenu.Display();
                    break;
                case "Exit":
                    circle = false;
                    break;
            }
        }
    }
}
