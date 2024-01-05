using OnlineCourseManagementSystem.Interfaces;
using OnlineCourseManagementSystem.Models;

namespace OnlineCourseManagementSystem.Services;

public class EnrollmentService : IEnrollmentService
{
    private readonly List<Enrollment> enrollments;
    private readonly CourseService courseService;
    private readonly StudentService studentService;

    public EnrollmentService(CourseService courseService, StudentService studentService)
    {
        this.courseService = courseService;
        this.studentService = studentService;
        this.enrollments = new List<Enrollment>();
    }

    public Enrollment Add(Enrollment enrollment)
    {
        var course = courseService.GetById(enrollment.CourseId);
        var student = studentService.GetById(enrollment.StudentId);

        student.EnrolledCourses.Add(course);
        student.Grades.Add(course, 0.0m);
        course.Students.Add(student);
        enrollments.Add(enrollment);
        return enrollment;
    }

    public bool Delete(int id)
    {
        var enrollment = enrollments.FirstOrDefault(e => e.Id == id)
            ?? throw new Exception("Enrollment with this id was not found");

        var student = studentService.GetById(enrollment.StudentId);
        var course = courseService.GetById(enrollment.CourseId);

        student.EnrolledCourses.Remove(course);
        course.Students.Remove(student);

        return enrollments.Remove(enrollment);
    }

    public List<Enrollment> GetAll()
        => enrollments;

    public Enrollment GetById(int id)
    {
        var enrollment = enrollments.FirstOrDefault(e => e.Id == id)
            ?? throw new Exception("Enrollment with this id was not found");

        return enrollment;
    }
}
