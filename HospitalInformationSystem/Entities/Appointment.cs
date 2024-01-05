namespace HospitalInformationSystem.Entities;

public class Appointment
{
    private static int id = 0;

    public Appointment()
    {
        this.Id = ++id;
    }

    public int Id { get; set; }
    public int PatientId { get; set; }
    public int DoctorId { get; set; }
    public DateTime Time { get; set; }
}