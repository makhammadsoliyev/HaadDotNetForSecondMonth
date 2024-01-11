using StudyTrackerSystem.Models;

namespace StudyTrackerSystem.Interfaces;

public interface ICourseService
{
    Course Add(Course course);
    Course GetById(int id);
    Course Update(int id, Course course);
    bool Delete(int id);
    List<Course> GetAll();
}
