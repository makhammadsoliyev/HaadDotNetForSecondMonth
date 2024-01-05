namespace HospitalInformationSystem.Entities;

public class Doctor
{
    private static int id = 0;

    public Doctor()
    {
        this.Id = ++id;
    }

    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Specialization { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public List<Appointment> Appointments { get; set; } = new List<Appointment>();
}
