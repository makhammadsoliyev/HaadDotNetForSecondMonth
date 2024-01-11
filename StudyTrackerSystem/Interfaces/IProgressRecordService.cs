using StudyTrackerSystem.Models;

namespace StudyTrackerSystem.Interfaces;

public interface IProgressRecordService
{
    ProgressRecord Add(ProgressRecord record);
    ProgressRecord GetById(int id);
    ProgressRecord Update(int id, ProgressRecord record);
    bool Delete(int id);
    List<ProgressRecord> GetAll();
    List<ProgressRecord> GetProgressRecordsByCourse(int courseId);
    List<ProgressRecord> GetProgressRecordsByStudent(int studentId);
}
