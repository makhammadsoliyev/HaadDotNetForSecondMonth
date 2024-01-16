using System.Text;
using TravelBookingSystem.Configurations;
using TravelBookingSystem.Interfaces;
using TravelBookingSystem.Models;

namespace TravelBookingSystem.Services;

public class PaymentService : IPaymentService
{
    private readonly BookingService bookingService;

    public PaymentService(BookingService bookingService)
    {
        this.bookingService = bookingService;
    }

    public async Task<Payment> Add(Payment payment)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> Delete(int id)
    {
        var payments = await GetAll();
        var payment = payments.FirstOrDefault(p => p.Id == id)
            ?? throw new Exception("Payment with this id was not found");

        StringBuilder sb = new StringBuilder();
        foreach (var p in payments)
            sb.AppendLine($"{p.Id}|{p.BookingId}|{p.Status}|{p.TotalAmount}");

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
        throw new NotImplementedException();
    }

    public async Task<Payment> Update(int id, Payment payment)
    {
        throw new NotImplementedException();
    }
}
