using InventoryManagementSystem.Entities;

namespace InventoryManagementSystem.Interfaces;

public interface ICustomerService
{
    Customer Add(Customer customer);
    Customer GetById(int id);
    Customer Update(int id, Customer customer);
    bool Delete(int id);
    List<Customer> GetAll();
}
