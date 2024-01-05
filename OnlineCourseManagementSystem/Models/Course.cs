namespace OnlineCourseManagementSystem.Models;

public class Course
{
    private static int _id = 0;

    public Course()
    {
        this.Id = ++_id;
        this.Students = new List<Student>();
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public Instructor Instructor { get; set; }
    public List<Student> Students { get; set; }
}
