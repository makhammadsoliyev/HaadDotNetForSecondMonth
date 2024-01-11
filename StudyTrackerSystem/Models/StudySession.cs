namespace StudyTrackerSystem.Models;

public class StudySession : Auditable
{
    private static int id = 0;

    public StudySession()
    {
        Id = ++id;
    }

    public int StudentId { get; set; }
    public int CourseId { get; set; }
    public DateOnly StartDate { get; set; }
    public int DurationInMinutes { get; set; }
    public string StudyMaterialsCovered { get; set; }
    public string Notes { get; set; }
}
