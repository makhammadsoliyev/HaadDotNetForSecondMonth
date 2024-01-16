using System.Text;
using TravelBookingSystem.Configurations;
using TravelBookingSystem.Interfaces;
using TravelBookingSystem.Models;

namespace TravelBookingSystem.Services;

public class PaymentService : IPaymentService
{
    private readonly BookingService bookingService;
    private readonly TravelPackageService travelPackageService;

    public PaymentService(BookingService bookingService, TravelPackageService travelPackageService)
    {
        this.bookingService = bookingService;
        this.travelPackageService = travelPackageService;
    }

    public async Task<Payment> Add(Payment payment)
    {
        var booking = await bookingService.GetById(payment.BookingId);
        var package = await travelPackageService.GetById(booking.PackageId);

        if (booking.NumberOfTravelers * package.Price > payment.TotalAmount)
            throw new Exception($"This amount is not enough for {booking.NumberOfTravelers} travelers with package price which is {package.Price} for per person");

        payment.Status = PaymentStatus.Completed;

        File.AppendAllText(Constants.PAYMENTS_PATH, $"{payment.Id}|{payment.BookingId}|{payment.Status}|{payment.TotalAmount}\n");
        return payment;
    }

    public async Task<bool> Delete(int id)
    {
        var payments = await GetAll();
        var payment = payments.FirstOrDefault(p => p.Id == id)
            ?? throw new Exception("Payment with this id was not found");

        StringBuilder sb = new StringBuilder();
        foreach (var p in payments)
        {
            if (p.Id == id)
                continue;

            sb.AppendLine($"{p.Id}|{p.BookingId}|{p.Status}|{p.TotalAmount}");
        }

        File.WriteAllText(Constants.PAYMENTS_PATH, sb.ToString());
        return true;
    }

    public async Task<List<Payment>> GetAll()
    {
        var data = File.ReadAllLines(Constants.PAYMENTS_PATH);
        var payments = new List<Payment>();

        foreach (var line in data)
        {
            var paymentData = line.Split('|');
            var payment = new Payment()
            {
                Id = Convert.ToInt32(paymentData[0]),
                BookingId = Convert.ToInt32(paymentData[1]),
                Status = (PaymentStatus)Enum.Parse(typeof(PaymentStatus), paymentData[2]),
                TotalAmount = Convert.ToDecimal(paymentData[3]),
            };

            payments.Add(payment);
        }

        return payments;
    }

    public async Task<Payment> GetById(int id)
    {
        var payments = await GetAll();
        var payment = payments.FirstOrDefault(p => p.Id == id)
            ?? throw new Exception("Payment with this id was not found");

        return payment;
    }

    public async Task<Payment> Update(int id, Payment payment)
    {
        var payments = await GetAll();
        var existPayment = payments.FirstOrDefault(p => p.Id == id)
            ?? throw new Exception("Payment with this id was not found");

        var booking = await bookingService.GetById(payment.BookingId);
        var package = await travelPackageService.GetById(booking.PackageId);

        if (booking.NumberOfTravelers * package.Price > payment.TotalAmount)
            throw new Exception($"This amount is not enough for {booking.NumberOfTravelers} travelers with package price which is {package.Price} for per person");


        existPayment.Id = id;
        existPayment.BookingId = payment.BookingId;
        existPayment.Status = PaymentStatus.Completed;
        existPayment.TotalAmount = payment.TotalAmount;

        StringBuilder sb = new StringBuilder();
        foreach (var p in payments)
            sb.AppendLine($"{p.Id}|{p.BookingId}|{p.Status}|{p.TotalAmount}");

        return existPayment;
    }
}
