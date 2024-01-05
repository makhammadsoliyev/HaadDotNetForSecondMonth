using OnlineCourseManagementSystem.Interfaces;
using OnlineCourseManagementSystem.Models;

namespace OnlineCourseManagementSystem.Services;

public class CourseService : ICourseService
{
    private readonly List<Course> courses;

    public CourseService()
    {
        this.courses = new List<Course>();
    }

    public Course Add(Course course)
    {
        if (course.EndDate <= course.StartDate)
            throw new Exception("EndDate must be bigger than StartDate");

        courses.Add(course);
        return course;
    }

    public bool Delete(int id)
    {
        var course = courses.FirstOrDefault(c => c.Id == id)
            ?? throw new Exception("Course with this id was not found");

        return courses.Remove(course);
    }

    public List<Student> EnrollmentSummary(int id)
    {
        var course = courses.FirstOrDefault(c => c.Id == id)
            ?? throw new Exception("Course with this id was not found");

        return course.Students;
    }

    public List<Course> GetAll()
        => courses;

    public Course GetById(int id)
    {
        var course = courses.FirstOrDefault(c => c.Id == id)
            ?? throw new Exception("Course with this id was not found");

        return course;
    }

    public List<Course> SearchCoursesByInstructorName(string name)
        => courses.Where(c => $"{c.Instructor.FirstName} {c.Instructor.LastName}".Equals(name)).ToList();

    public Course Update(int id, Course course)
    {
        var existCourse = courses.FirstOrDefault(c => c.Id == id)
            ?? throw new Exception("Course with this id was not found");

        existCourse.Id = id;
        existCourse.Name = course.Name;
        existCourse.EndDate = course.EndDate;
        existCourse.StartDate = course.StartDate;
        existCourse.Instructor = course.Instructor;
        existCourse.Description = course.Description;

        return existCourse;
    }
}
