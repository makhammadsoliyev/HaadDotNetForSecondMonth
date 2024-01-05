using HospitalInformationSystem.Entities;
using HospitalInformationSystem.Interfaces;

namespace HospitalInformationSystem.Services;

public class PatientService : IPatientService
{
    private readonly List<Patient> patients;

    public PatientService()
    {
        this.patients = new List<Patient>();
    }

    public Patient Add(Patient patient)
    {
        var existPatient = patients.FirstOrDefault(p => p.Phone == patient.Phone);
        if (existPatient is not null)
            throw new Exception("Patient with this phone already exists...");

        patients.Add(patient);
        return patient;
    }

    public bool Delete(int id)
    {
        var patient = patients.FirstOrDefault(p => p.Id == id)
            ?? throw new Exception("Patient with this id was not found...");

        return patients.Remove(patient);
    }

    public List<Patient> FindAllByAge(uint age)
        => patients.Where(p => DateTime.Now.Year - p.DateOfBirth.Year == age).ToList();

    public List<Patient> FindAllByName(string name)
        => patients.Where(p => $"{p.FirstName} {p.LastName}".Equals(name)).ToList();

    public List<Patient> GetAll()
        => patients;

    public Patient GetById(int id)
    {
        var patient = patients.FirstOrDefault(p => p.Id == id)
            ?? throw new Exception("Patient with this id was not found...");

        return patient;
    }

    public Patient Update(int id, Patient patient)
    {
        var existPatient = patients.FirstOrDefault(p => p.Id == id)
            ?? throw new Exception("Patient with this id was not found...");

        existPatient.Id = id;
        existPatient.Phone = patient.Phone;
        existPatient.Gender = patient.Gender;
        existPatient.Address = patient.Address;
        existPatient.LastName = patient.LastName;
        existPatient.FirstName = patient.FirstName;
        existPatient.DateOfBirth = patient.DateOfBirth;
        existPatient.MedicalHistory = patient.MedicalHistory;

        return existPatient;
    }
}
