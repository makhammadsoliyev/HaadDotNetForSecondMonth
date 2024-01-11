namespace StudyTrackerSystem.Models;

public class Course : Auditable
{
    private static int id = 0;

    public Course()
    {
        Id = ++id;
    }

    public string Name { get; set; }
    public string Description { get; set; }
    public string InstructorName { get; set; }
    public string Schedule { get; set; }
    public int Credits { get; set; }
}
