using HospitalInformationSystem.Entities;

namespace HospitalInformationSystem.Interfaces;

public interface IMedicalRecordService
{
    MedicalRecord Add(MedicalRecord record);
    MedicalRecord GetById(int id);
    MedicalRecord Update(int id, MedicalRecord record);
    bool Delete(int id);
    List<MedicalRecord> GetAll();
}
