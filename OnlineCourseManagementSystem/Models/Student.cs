namespace OnlineCourseManagementSystem.Models;

public class Student
{
    private static int _id = 0;

    public Student()
    {
        this.Id = ++_id;
        this.EnrolledCourses = new List<Course>();
        this.Grades = new Dictionary<Course, decimal>();
    }

    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public List<Course> EnrolledCourses { get; set; }
    public Dictionary<Course, decimal> Grades { get; set; }
}
