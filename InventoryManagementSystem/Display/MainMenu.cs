using InventoryManagementSystem.Services;
using Spectre.Console;

namespace InventoryManagementSystem.Display;

public class MainMenu
{
    private readonly string salePath;
    private readonly string productPath;
    private readonly string customerPath;
    private readonly string supplierPath;

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
        this.productPath = @"C:\Users\User\Desktop\dotnet\HaadDotNetForSecondMonth\InventoryManagementSystem\DataBases\products.txt";
        this.salePath = @"C:\Users\User\Desktop\dotnet\HaadDotNetForSecondMonth\InventoryManagementSystem\DataBases\sales.txt";
        this.customerPath = @"C:\Users\User\Desktop\dotnet\HaadDotNetForSecondMonth\InventoryManagementSystem\DataBases\customers.txt";
        this.supplierPath = @"C:\Users\User\Desktop\dotnet\HaadDotNetForSecondMonth\InventoryManagementSystem\DataBases\suppliers.txt";

        this.supplierService = new SupplierService(supplierPath);
        this.customerService = new CustomerService(customerPath);
        this.productService = new ProductService(supplierService, productPath);
        this.saleService = new SaleService(customerService, productService, salePath);

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
            var selection = selectionDisplay.ShowSelectionMenu("Choose one of options", new string[] { "Customer", "Supplier", "Product", "Sale", "Exit" });

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
