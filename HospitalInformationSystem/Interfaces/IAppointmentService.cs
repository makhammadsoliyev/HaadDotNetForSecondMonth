using HospitalInformationSystem.Entities;

namespace HospitalInformationSystem.Interfaces;

public interface IAppointmentService
{
    Appointment Add(Appointment appointment);
    Appointment GetById(int id);
    Appointment Update(int id, Appointment appointment);
    bool Delete(int id);
    List<Appointment> GetAll();
    List<Appointment> GetAllByDate(DateOnly date);
}
