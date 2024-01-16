using System.Text;
using TravelBookingSystem.Configurations;
using TravelBookingSystem.Interfaces;
using TravelBookingSystem.Models;

namespace TravelBookingSystem.Services;

public class CustomerService : ICustomerService
{
    public async Task<Customer> Add(Customer customer)
    {
        var customers = await GetAll();
        var existCustomer = customers.FirstOrDefault(c => c.Phone == customer.Phone);
        if (existCustomer is not null)
            throw new Exception("Customer with this phone already exists");

        File.AppendAllText(Constants.CUSTOMERS_PATH, $"{customer.Id}|{customer.Phone}|{customer.FirstName}|{customer.LastName}|{customer.Phone}\n");
        return customer;
    }

    public async Task<bool> Delete(int id)
    {
        var customers = await GetAll();
        var customer = customers.FirstOrDefault(c => c.Id == id)
            ?? throw new Exception("Customer with this id was not found");

        StringBuilder sb = new StringBuilder();
        foreach (var c in customers)
        {
            if (c.Id == id)
                continue;
            sb.AppendLine($"{c.Id}|{c.FirstName}|{c.LastName}|{c.Phone}");
        }

        File.WriteAllText(Constants.CUSTOMERS_PATH, sb.ToString());
        return true;
    }

    public async Task<List<Customer>> GetAll()
    {
        var data = File.ReadAllLines(Constants.CUSTOMERS_PATH);
        var customers = new List<Customer>();

        foreach (var line in data)
        {
            var customerData = line.Split('|');
            var customer = new Customer()
            {
                Id = Convert.ToInt32(customerData[0]),
                FirstName = customerData[1],
                LastName = customerData[2],
                Phone = customerData[3]
            };

            customers.Add(customer);
        }

        return customers;
    }

    public async Task<Customer> GetById(int id)
    {
        var customers = await GetAll();
        var customer = customers.FirstOrDefault(c => c.Id == id)
            ?? throw new Exception("Customer with this id was not found");

        return customer;
    }

    public async Task<Customer> Update(int id, Customer customer)
    {
        var customers = await GetAll();
        var existCustomer = customers.FirstOrDefault(c => c.Id == id)
            ?? throw new Exception("Customer with this id was not found");

        existCustomer.Id = id;
        existCustomer.Phone = customer.Phone;
        existCustomer.LastName = customer.LastName;
        existCustomer.FirstName = customer.FirstName;

        StringBuilder sb = new StringBuilder();
        foreach (var c in customers)
            sb.AppendLine($"{c.Id}|{c.Phone}|{c.FirstName}|{c.LastName}|{c.Phone}");

        File.WriteAllText(Constants.CUSTOMERS_PATH, sb.ToString());
        return existCustomer;
    }
}
