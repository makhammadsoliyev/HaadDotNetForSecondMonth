using StudyTrackerSystem.Enums;

namespace StudyTrackerSystem.Models;

public class StudyPlan : Auditable
{
    private static int id = 0;

    public StudyPlan()
    {
        Id = ++id;
    }

    public int StudentId { get; set; }
    public int CourseId { get; set; }
    public string Materials { get; set; }
    public string Topics { get; set; }
    public string Goals { get; set; }
    public int StudyHoursPerWeek { get; set; }
    public DayOfWeek DayOff { get; set; }
    public PartOfDays PreferredStudyTime { get; set; }
}
