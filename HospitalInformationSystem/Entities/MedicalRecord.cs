namespace HospitalInformationSystem.Entities;

public class MedicalRecord
{
    private static int id = 0;
    public MedicalRecord()
    {
        this.Id = ++id;
    }

    public int Id { get; set; }
    public string MedicalConditions { get; set; }
    public string Medications { get; set; }
    public string TestResults { get; set; }
    public string TreatmentPlans { get; set; }
    public int PatientId { get; set; }
    public int DoctorId { get; set; }
}
