using TravelBookingSystem.Models;

namespace TravelBookingSystem.Interfaces;

public interface ICustomerService
{
    Task<Customer> Add(Customer customer);
    Task<Customer> GetById(int id);
    Task<Customer> Update(int id, Customer customer);
    Task<bool> Delete(int id);
    Task<List<Customer>> GetAll();
    Task<List<Customer>> SearchByName(string fullName);
}
