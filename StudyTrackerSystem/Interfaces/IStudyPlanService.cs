using StudyTrackerSystem.Models;

namespace StudyTrackerSystem.Interfaces;

public interface IStudyPlanService
{
    StudyPlan Add(StudyPlan studyPlan);
    StudyPlan GetById(int id);
    StudyPlan Update(int id, StudyPlan studyPlan);
    bool Delete(int id);
    List<StudyPlan> GetAll();
    List<StudyPlan> GetStudyPlansByCourse(int courseId);
    List<StudyPlan> GetStudyPlansByStudent(int studentId);
}
