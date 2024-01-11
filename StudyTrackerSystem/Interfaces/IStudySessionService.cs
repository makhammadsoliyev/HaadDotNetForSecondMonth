using StudyTrackerSystem.Models;

namespace StudyTrackerSystem.Interfaces;

public interface IStudySessionService
{
    StudySession Add(StudySession session);
    StudySession GetById(int id);
    StudySession Update(int id, StudySession session);
    bool Delete(int id);
    List<StudySession> GetAll();
    List<StudySession> GetStudySessionsByStudent(int studentId);
    List<StudySession> GetStudySessionsByCourse(int courseId);
    List<StudySession> GetUpcomingStudySession();
}
