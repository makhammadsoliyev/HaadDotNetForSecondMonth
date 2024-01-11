namespace StudyTrackerSystem.Models;

public class ProgressRecord : Auditable
{
    private static int id = 0;

    public ProgressRecord()
    {
        Id = ++id;
    }

    public int StudentId { get; set; }
    public int CourseId { get; set; }
    public string ProgressDetails { get; set; }
}
