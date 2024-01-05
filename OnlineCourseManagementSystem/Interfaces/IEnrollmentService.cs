using OnlineCourseManagementSystem.Models;

namespace OnlineCourseManagementSystem.Interfaces;

public interface IEnrollmentService
{
    Enrollment Add(Enrollment enrollment);
    Enrollment GetById(int id);
    bool Delete(int id);
    List<Enrollment> GetAll();
}
