using InventoryManagementSystem.Entities;
using InventoryManagementSystem.Interfaces;
using System.Drawing;

namespace InventoryManagementSystem.Services;

public class SaleService : ISaleService
{
    private readonly List<Sale> sales;
    private readonly ProductService productService;
    private readonly CustomerService customerService;

    public SaleService(CustomerService customerService, ProductService productService)
    {
        this.sales = new List<Sale>();
        this.productService = productService;
        this.customerService = customerService;
    }

    public Sale Add(Sale sale)
    {
        var product = productService.GetById(sale.ProductId);
        var customer = customerService.GetById(sale.CustomerId);

        if (product.StockQuantity > sale.Quantity)
            throw new Exception("This amount of product is not available");
        product.StockQuantity -= sale.Quantity;
        customer.PurchasedProducts.Add(product);
        sales.Add(sale);
        return sale;
    }

    public bool Delete(int id)
    {
        var sale = sales.FirstOrDefault(s => s.Id == id)
            ?? throw new Exception("Sale with this id was not found");

        return sales.Remove(sale);
    }

    public List<Sale> GetAll()
        => sales;

    public Sale GetById(int id)
    {
        var sale = sales.FirstOrDefault(s => s.Id == id)
            ?? throw new Exception("Sale with this id was not found");

        return sale;
    }

    public Sale Update(int id, Sale sale)
    {
        var existSale = sales.FirstOrDefault(s => s.Id == id)
            ?? throw new Exception("Sale with this id was not found");

        var product = productService.GetById(existSale.ProductId);
        var customer = customerService.GetById(existSale.CustomerId);
        customer.PurchasedProducts.Remove(product);

        var newCustomer = customerService.GetById(sale.CustomerId);
        newCustomer.PurchasedProducts.Add(product);

        if (sale.Quantity > existSale.Quantity + product.StockQuantity)
            throw new Exception("This amount of product is not available");

        existSale.Id = id;
        existSale.ProductId = sale.ProductId;
        existSale.CustomerId = sale.CustomerId;
        existSale.Quantity -= sale.Quantity - existSale.Quantity;

        return existSale;
    }
}
