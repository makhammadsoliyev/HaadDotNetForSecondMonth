using HospitalInformationSystem.Entities;
using HospitalInformationSystem.Interfaces;

namespace HospitalInformationSystem.Services;

public class AppointmentService : IAppointmentService
{
    private readonly DoctorService doctorService;
    private readonly PatientService patientService;
    private readonly List<Appointment> appointments;

    public AppointmentService(PatientService patientService, DoctorService doctorService)
    {
        this.doctorService = doctorService;
        this.patientService = patientService;
        this.appointments = new List<Appointment>();
    }

    public Appointment Add(Appointment appointment)
    {
        var doctor = doctorService.GetById(appointment.DoctorId);
        var patient = patientService.GetById(appointment.PatientId);

        var appointmentAvailability =
            doctor.Appointments.FirstOrDefault(a => a.Time.AddMinutes(30) == appointment.Time ||
            a.Time.AddMinutes(-30) == appointment.Time);

        if (appointmentAvailability is not null)
            throw new Exception("Appointment is not available for this time...");

        doctor.Appointments.Add(appointment);
        appointments.Add(appointment);
        return appointment;
    }

    public bool Delete(int id)
    {
        var appointment = appointments.FirstOrDefault(a => a.DoctorId == id)
            ?? throw new Exception("Appointment with this id was not found...");
        var doctor = doctorService.GetById(appointment.DoctorId);

        doctor.Appointments.Remove(appointment);
        return appointments.Remove(appointment);
    }

    public List<Appointment> GetAll()
        => appointments;

    public List<Appointment> GetAllByDate(DateOnly date)
        => appointments.Where(a => a.Time.Date.Equals(date)).ToList();

    public Appointment GetById(int id)
    {
        var appointment = appointments.FirstOrDefault(a => a.Id == id)
            ?? throw new Exception("Appointment with this id was not found...");

        return appointment;
    }

    public Appointment Update(int id, Appointment appointment)
    {
        var existAppointment = appointments.FirstOrDefault(a => a.Id == id)
            ?? throw new Exception("Appointment with this id was not found...");

        existAppointment.Id = id;
        existAppointment.Time = appointment.Time;
        existAppointment.DoctorId = appointment.DoctorId;
        existAppointment.PatientId = appointment.PatientId;

        return existAppointment;
    }
}
