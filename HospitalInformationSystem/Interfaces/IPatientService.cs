using HospitalInformationSystem.Entities;

namespace HospitalInformationSystem.Interfaces;

public interface IPatientService
{
    Patient Add(Patient patient);
    Patient GetById(int id);
    Patient Update(int id, Patient patient);
    bool Delete(int id);
    List<Patient> GetAll();
    List<Patient> FindAllByAge(uint age);
    List<Patient> FindAllByName(string name);
}
