using TravelBookingSystem.Models;

namespace TravelBookingSystem.Interfaces;

public interface IBookingService
{
    Task<Booking> Add(Booking booking);
    Task<Booking> GetById(int id);
    Task<Booking> Update(int id, Booking booking);
    Task<bool> Delete(int id);
    Task<List<Booking>> GetAll();
}
