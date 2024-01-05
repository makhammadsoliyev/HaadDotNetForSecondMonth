using HospitalInformationSystem.Entities;
using HospitalInformationSystem.Interfaces;

namespace HospitalInformationSystem.Services;

public class DoctorService : IDoctorService
{
    private readonly List<Doctor> doctors;

    public DoctorService()
    {
        this.doctors = new List<Doctor>();
    }

    public Doctor Add(Doctor doctor)
    {
        var existDoctor = doctors.FirstOrDefault(d => d.Phone.Equals(doctor.Phone));

        if (existDoctor is not null)
            throw new Exception("Doctor with this phone already exists...");

        doctors.Add(doctor);
        return doctor;
    }

    public bool Delete(int id)
    {
        var doctor = doctors.FirstOrDefault(d => d.Id == id)
            ?? throw new Exception("Doctor with this id was not found...");

        return doctors.Remove(doctor);
    }

    public List<Doctor> GetAll()
        => doctors;

    public List<Doctor> GetAllByName(string name)
        => doctors.Where(d => $"{d.FirstName} {d.LastName}".Equals(name)).ToList();

    public List<Doctor> GetAllBySpecialization(string specialization)
        => doctors.Where(d => d.Specialization.Equals(specialization)).ToList();

    public Doctor GetById(int id)
    {
        var doctor = doctors.FirstOrDefault(d => d.Id == id)
            ?? throw new Exception("Doctor with this id was not found...");

        return doctor;
    }

    public Doctor Update(int id, Doctor doctor)
    {
        var existDoctor = doctors.FirstOrDefault(d => d.Id == id)
            ?? throw new Exception("Doctor with this id was not found...");

        existDoctor.Id = id;
        existDoctor.Phone = doctor.Phone;
        existDoctor.Address = doctor.Address;
        existDoctor.LastName = doctor.LastName;
        existDoctor.FirstName = doctor.FirstName;
        existDoctor.Specialization = doctor.Specialization;

        return existDoctor;
    }
}
