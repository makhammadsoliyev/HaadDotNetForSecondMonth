using HospitalInformationSystem.Entities;
using HospitalInformationSystem.Interfaces;

namespace HospitalInformationSystem.Services;

public class MedicalRecordService : IMedicalRecordService
{
    private readonly List<MedicalRecord> records;
    private readonly DoctorService doctorService;
    private readonly PatientService patientService;

    public MedicalRecordService(DoctorService doctorService, PatientService patientService)
    {
        this.doctorService = doctorService;
        this.patientService = patientService;
        this.records = new List<MedicalRecord>();
    }

    public MedicalRecord Add(MedicalRecord record)
    {
        var doctor = doctorService.GetById(record.DoctorId);
        var patient = patientService.GetById(record.PatientId);

        records.Add(record);
        return record;
    }

    public bool Delete(int id)
    {
        var record = records.FirstOrDefault(r => r.Id == id)
            ?? throw new Exception("Record with this id was not found...");

        return records.Remove(record);

    }

    public List<MedicalRecord> GetAll()
        => records;

    public MedicalRecord GetById(int id)
    {
        var record = records.FirstOrDefault(r => r.Id == id)
            ?? throw new Exception("Record with this id was not found...");

        return record;
    }

    public MedicalRecord Update(int id, MedicalRecord record)
    {
        var existRecord = records.FirstOrDefault(r => r.Id == record.Id)
            ?? throw new Exception("Record with this id was not found...");

        existRecord.Id = id;
        existRecord.DoctorId = record.DoctorId;
        existRecord.PatientId = record.PatientId;
        existRecord.TestResults = record.TestResults;
        existRecord.Medications = record.Medications;
        existRecord.TreatmentPlans = record.TreatmentPlans;
        existRecord.MedicalConditions = record.MedicalConditions;

        return existRecord;
    }
}
