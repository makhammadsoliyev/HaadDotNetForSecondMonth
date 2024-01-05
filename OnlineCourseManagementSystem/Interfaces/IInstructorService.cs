using OnlineCourseManagementSystem.Models;

namespace OnlineCourseManagementSystem.Interfaces;

public interface IInstructorService
{
    Instructor Add(Instructor instructor);
    Instructor GetById(int id);
    Instructor Update(int id, Instructor instructor);
    bool Delete(int id);
    List<Instructor> GetAll();
    Student GradeAssignment(int studentId, int courseId, decimal grade);
}
