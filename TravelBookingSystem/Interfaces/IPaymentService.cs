using TravelBookingSystem.Models;

namespace TravelBookingSystem.Interfaces;

public interface IPaymentService
{
    Task<Payment> Add(Payment payment);
    Task<Payment> GetById(int id);
    Task<Payment> Update(int id, Payment payment);
    Task<bool> Delete(int id);
    Task<List<Payment>> GetAll();
}
