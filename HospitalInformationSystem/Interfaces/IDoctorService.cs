using HospitalInformationSystem.Entities;

namespace HospitalInformationSystem.Interfaces;

public interface IDoctorService
{
    Doctor Add(Doctor doctor);
    Doctor GetById(int id);
    Doctor Update(int id, Doctor doctor);
    bool Delete(int id);
    List<Doctor> GetAll();
    List<Doctor> GetAllBySpecialization(string specialization);
    List<Doctor> GetAllByName(string name);
}
