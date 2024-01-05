namespace OnlineCourseManagementSystem.Models;

public class Enrollment
{
    private static int _id = 0;

    public Enrollment()
    {
        this.Id = ++_id;
    }

    public int Id { get; set; }
    public int CourseId { get; set; }
    public int StudentId { get; set; }
}
