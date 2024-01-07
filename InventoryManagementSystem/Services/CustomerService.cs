using InventoryManagementSystem.Entities;
using InventoryManagementSystem.Interfaces;

namespace InventoryManagementSystem.Services;

public class CustomerService : ICustomerService
{
    private readonly List<Customer> customers;

    public CustomerService()
    {
        this.customers = new List<Customer>();
    }

    public Customer Add(Customer customer)
    {
        var existCustomer = customers.FirstOrDefault(c => c.PhoneNumber.Equals(customer.PhoneNumber));
        if (existCustomer is not null)
            throw new Exception("Customer with this phone number already exists");

        customers.Add(customer);
        return customer;
    }

    public bool Delete(int id)
    {
        var customer = customers.FirstOrDefault(c => c.Id == id)
            ?? throw new Exception("Customer with this id was not found");

        return customers.Remove(customer);
    }

    public List<Customer> GetAll()
        => customers;

    public Customer GetById(int id)
    {
        var customer = customers.FirstOrDefault(c => c.Id == id)
            ?? throw new Exception("Customer with this id was not found");

        return customer;
    }

    public Customer Update(int id, Customer customer)
    {
        var existCustomer = customers.FirstOrDefault(c => c.Id == id)
            ?? throw new Exception("Customer with this id was not found");

        existCustomer.Id = id;
        existCustomer.LastName = customer.LastName;
        existCustomer.FirstName = customer.FirstName;
        existCustomer.PhoneNumber = customer.PhoneNumber;

        return existCustomer;
    }
}