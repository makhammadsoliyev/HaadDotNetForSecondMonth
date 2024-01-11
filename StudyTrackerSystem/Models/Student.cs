namespace StudyTrackerSystem.Models;

public class Student : Auditable
{
    private static int id = 0;

    public Student()
    {
        Id = ++id;
    }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}
