using InventoryManagementSystem.Entities;
using Spectre.Console;

namespace InventoryManagementSystem.Display;

public class SelectionMenu
{
    public Table DataTable(string title, params Product[] products)
    {
        var table = new Table();

        table.Title(title.ToUpper())
            .BorderColor(Color.Blue)
            .AsciiBorder();

        table.AddColumn("ID");
        table.AddColumn("Name");
        table.AddColumn("Description");
        table.AddColumn("Price");
        table.AddColumn("StockQuantity");
        table.AddColumn("SupplierId");

        foreach (var product in products)
            table.AddRow(product.Id.ToString(), product.Name, product.Description, $"{product.Price:C}", product.StockQuantity.ToString(), product.SupplierId.ToString());

        return table;
    }

    public Table DataTable(string title, params Supplier[] suppliers)
    {
        var table = new Table();

        table.Title(title.ToUpper())
            .BorderColor(Color.Blue)
            .AsciiBorder();

        table.AddColumn("ID");
        table.AddColumn("FirstName");
        table.AddColumn("LastName");
        table.AddColumn("PhoneNumber");
        table.AddColumn("Number of supplied products");

        foreach (var supplier in suppliers)
            table.AddRow(supplier.Id.ToString(), supplier.FirstName, supplier.LastName, supplier.PhoneNumber, supplier.Products.Count.ToString());

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
        table.AddColumn("PhoneNumber");
        table.AddColumn("Number of purchased products");

        foreach (var customer in customers)
            table.AddRow(customer.Id.ToString(), customer.FirstName, customer.LastName, customer.PhoneNumber, customer.PurchasedProducts.Count.ToString());

        return table;
    }

    public Table DataTable(string title, params Sale[] sales)
    {
        var table = new Table();

        table.Title(title.ToUpper())
            .BorderColor(Color.Blue)
            .AsciiBorder();

        table.AddColumn("ID");
        table.AddColumn("ProductId");
        table.AddColumn("CustomerId");
        table.AddColumn("Quantity");
        table.AddColumn("SoldDate");

        foreach (var sale in sales)
            table.AddRow(sale.Id.ToString(), sale.ProductId.ToString(), sale.CustomerId.ToString(), sale.Quantity.ToString(), sale.SoldDate.ToString());

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
