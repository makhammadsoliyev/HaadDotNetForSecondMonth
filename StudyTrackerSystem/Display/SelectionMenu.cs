using Spectre.Console;
using StudyTrackerSystem.Models;

namespace StudyTrackerSystem.Display;

public class SelectionMenu
{
    public Table DataTable(string title, params StudyPlan[] studyPlans)
    {
        var table = new Table();

        table.Title(title.ToUpper())
            .BorderColor(Color.Blue)
            .AsciiBorder();

        table.AddColumn("ID");
        table.AddColumn("CourseId");
        table.AddColumn("StudentId");
        table.AddColumn("Materials");
        table.AddColumn("Topics");
        table.AddColumn("Goals");
        table.AddColumn("StudyHoursPerWeek");
        table.AddColumn("DayOff");
        table.AddColumn("PreferredStudyTime");

        foreach (var plan in studyPlans)
            table.AddRow(plan.Id.ToString(), plan.CourseId.ToString(), plan.StudentId.ToString(), plan.Materials, plan.Topics, plan.Goals, plan.StudyHoursPerWeek.ToString(), plan.DayOff.ToString(), plan.PreferredStudyTime.ToString());

        return table;
    }

    public Table DataTable(string title, params StudySession[] studySessions)
    {
        var table = new Table();

        table.Title(title.ToUpper())
            .BorderColor(Color.Blue)
            .AsciiBorder();

        table.AddColumn("ID");
        table.AddColumn("CourseId");
        table.AddColumn("StudentId");
        table.AddColumn("StartDate");
        table.AddColumn("DurationInMinutes");
        table.AddColumn("StudyMaterialsCovered");
        table.AddColumn("Notes");

        foreach (var session in studySessions)
            table.AddRow(session.Id.ToString(), session.CourseId.ToString(), session.StudentId.ToString(), session.StartDate.ToString(), session.DurationInMinutes.ToString(), session.StudyMaterialsCovered, session.Notes);

        return table;
    }

    public Table DataTable(string title, params ProgressRecord[] progressRecords)
    {
        var table = new Table();

        table.Title(title.ToUpper())
            .BorderColor(Color.Blue)
            .AsciiBorder();

        table.AddColumn("ID");
        table.AddColumn("CourseId");
        table.AddColumn("StudentId");
        table.AddColumn("ProgressDetails");

        foreach (var record in progressRecords)
            table.AddRow(record.Id.ToString(), record.CourseId.ToString(), record.StudentId.ToString(), record.ProgressDetails);

        return table;
    }

    public Table DataTable(string title, params Student[] students)
    {
        var table = new Table();

        table.Title(title.ToUpper())
            .BorderColor(Color.Blue)
            .AsciiBorder();

        table.AddColumn("ID");
        table.AddColumn("FirstName");
        table.AddColumn("LastName");
        table.AddColumn("Email");
        table.AddColumn("Password");

        foreach (var student in students)
            table.AddRow(student.Id.ToString(), student.FirstName, student.LastName, student.Email, student.Password);

        return table;
    }

    public Table DataTable(string title, params Course[] courses)
    {
        var table = new Table();

        table.Title(title.ToUpper())
            .BorderColor(Color.Blue)
            .AsciiBorder();

        table.AddColumn("ID");
        table.AddColumn("Name");
        table.AddColumn("Description");
        table.AddColumn("InstructorName");
        table.AddColumn("Schedule");
        table.AddColumn("Credits");

        foreach (var course in courses)
            table.AddRow(course.Id.ToString(), course.Name, course.Description, course.InstructorName, course.Schedule, course.Credits.ToString());

        return table;
    }

    public string ShowSelectionMenu(string title, string[] options)
    {
        var selection = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title(title)
                .PageSize(5) // Number of items visible at once
                .AddChoices(options)
                .HighlightStyle(new Style(foreground: Color.Cyan2, background: Color.Blue))
        );

        return selection;
    }
}
