using OnlineCourseManagementSystem.Models;

namespace OnlineCourseManagementSystem.Interfaces;

public interface ICourseService
{
    Course Add(Course course);
    Course GetById(int id);
    Course Update(int id, Course course);
    bool Delete(int id);
    List<Course> GetAll();
    List<Course> SearchCoursesByInstructorName(string name);
    List<Student> EnrollmentSummary(int id);
}
