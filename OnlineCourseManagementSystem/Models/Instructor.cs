namespace OnlineCourseManagementSystem.Models;

public class Instructor
{
    private static int _id = 0;

    public Instructor()
    {
        this.Id = ++_id;
    }

    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public string Expertise { get; set; }
}
