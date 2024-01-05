using OnlineCourseManagementSystem.Interfaces;
using OnlineCourseManagementSystem.Models;

namespace OnlineCourseManagementSystem.Services;

public class InstructorService : IInstructorService
{
    private readonly List<Instructor> instructors;
    private readonly CourseService courseService;
    private readonly StudentService studentService;

    public InstructorService(StudentService studentService, CourseService courseService)
    {
        this.courseService = courseService;
        this.studentService = studentService;
        this.instructors = new List<Instructor>();
    }

    public Instructor Add(Instructor instructor)
    {
        var existInstructor = instructors.FirstOrDefault(i => i.Phone.Equals(instructor.Phone));
        if (existInstructor is not null)
            throw new Exception("Instructor with this phone already exists");

        instructors.Add(instructor);
        return instructor;
    }

    public bool Delete(int id)
    {
        var instructor = instructors.FirstOrDefault(i => i.Id == id)
            ?? throw new Exception("Instructor with this id was not found");

        return instructors.Remove(instructor);
    }

    public List<Instructor> GetAll()
        => instructors;

    public Instructor GetById(int id)
    {
        var instructor = instructors.FirstOrDefault(i => i.Id == id)
            ?? throw new Exception("Instructor with this id was not found");

        return instructor;
    }

    public Student GradeAssignment(int studentId, int courseId, decimal grade)
    {
        var student = studentService.GetById(studentId);
        var course = courseService.GetById(courseId);
        var courseOfStudent = student.EnrolledCourses.FirstOrDefault(c => c.Id == courseId)
            ?? throw new Exception("This student did not enroll this course");
        student.Grades[course] = grade;

        return student;
    }

    public Instructor Update(int id, Instructor instructor)
    {
        var existInstructor = instructors.FirstOrDefault(i => i.Id == id)
            ?? throw new Exception("Instructor with this id was not found");

        existInstructor.Id = id;
        existInstructor.Phone = existInstructor.Phone;
        existInstructor.LastName = instructor.LastName;
        existInstructor.FirstName = instructor.FirstName;
        existInstructor.Expertise = instructor.Expertise;

        return existInstructor;
    }
}
