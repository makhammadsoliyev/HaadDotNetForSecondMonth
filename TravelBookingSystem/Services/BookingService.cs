using System.Text;
using TravelBookingSystem.Configurations;
using TravelBookingSystem.Interfaces;
using TravelBookingSystem.Models;

namespace TravelBookingSystem.Services;

public class BookingService : IBookingService
{
    public readonly CustomerService customerService;
    public readonly TravelPackageService travelPackageService;

    public BookingService(CustomerService customerService, TravelPackageService travelPackageService)
    {
        this.customerService = customerService;
        this.travelPackageService = travelPackageService;
    }

    public async Task<Booking> Add(Booking booking)
    {
        var customer = await customerService.GetById(booking.CustomerId);
        var package = await travelPackageService.GetById(booking.PackageId);

        File.AppendAllText(Constants.BOOKINGS_PATH, $"{booking.Id}|{booking.PackageId}|{booking.CustomerId}|{booking.NumberOfTravelers}|{booking.TravelDate}\n");
        return booking;
    }

    public async Task<bool> Delete(int id)
    {
        var bookings = await GetAll();
        var booking = bookings.FirstOrDefault(c => c.Id == id)
            ?? throw new Exception("Customer with this id was not found");

        StringBuilder sb = new StringBuilder();
        foreach (var b in bookings)
        {
            if (b.Id == id)
                continue;
            sb.AppendLine($"{b.Id}|{b.PackageId}|{b.CustomerId}|{b.NumberOfTravelers}|{b.TravelDate}");
        }

        File.WriteAllText(Constants.BOOKINGS_PATH, sb.ToString());
        return true;
    }

    public async Task<List<Booking>> GetAll()
    {
        var data = File.ReadAllLines(Constants.BOOKINGS_PATH);
        var bookings = new List<Booking>();

        foreach (var line in data)
        {
            var bookingData = line.Split('|');
            var booking = new Booking()
            {
                Id = Convert.ToInt32(bookingData[0]),
                PackageId = Convert.ToInt32(bookingData[1]),
                CustomerId = Convert.ToInt32(bookingData[2]),
                NumberOfTravelers = Convert.ToInt32(bookingData[3]),
                TravelDate = DateOnly.Parse(bookingData[4])
            };

            bookings.Add(booking);
        }

        return bookings;
    }

    public async Task<List<Booking>> GetAllByCustomerId(int customerId)
    {
        var bookings = await GetAll();
        return bookings.FindAll(b => b.CustomerId == customerId);
    }

    public async Task<Booking> GetById(int id)
    {
        var bookings = await GetAll();
        var booking = bookings.FirstOrDefault(b => b.Id == id)
            ?? throw new Exception("Booking with this id was not found");

        return booking;
    }

    public async Task<Booking> Update(int id, Booking booking)
    {
        var bookings = await GetAll();
        var existBooking = bookings.FirstOrDefault(b => b.Id == id)
            ?? throw new Exception("Booking with this id was not found");
        var customer = await customerService.GetById(booking.CustomerId);
        var package = await travelPackageService.GetById(booking.PackageId);

        existBooking.Id = id;
        existBooking.PackageId = booking.PackageId;
        existBooking.CustomerId = booking.CustomerId;
        existBooking.TravelDate = booking.TravelDate;
        existBooking.NumberOfTravelers = booking.NumberOfTravelers;

        StringBuilder sb = new StringBuilder();
        foreach (var b in bookings)
        {
            sb.AppendLine($"{b.Id}|{b.PackageId}|{b.CustomerId}|{b.NumberOfTravelers}|{b.TravelDate}");
        }

        File.WriteAllText(Constants.BOOKINGS_PATH, sb.ToString());

        return existBooking;
    }
}
