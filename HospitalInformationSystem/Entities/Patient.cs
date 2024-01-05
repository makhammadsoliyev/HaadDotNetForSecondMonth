using HospitalInformationSystem.Enums;

namespace HospitalInformationSystem.Entities;

public class Patient
{
    private static int id = 0;

    public Patient()
    {
        this.Id = ++id;
    }

    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public string MedicalHistory { get; set; }
}
