using OnlineCourseManagementSystem.Models;
using Spectre.Console;

namespace OnlineCourseManagementSystem.Display;

public class SelectionMenu
{
    public Table DataTable(string title, params Student[] students)
    {
        var table = new Table();

        table.Title(title.ToUpper())
            .BorderColor(Color.Blue)
            .AsciiBorder();

        table.AddColumn("ID");
        table.AddColumn("FirstName");
        table.AddColumn("LastName");
        table.AddColumn("Phone");
        table.AddColumn("EnrolledCourses");
        table.AddColumn("Grades");

        foreach (var student in students)
            table.AddRow(student.Id.ToString(), student.FirstName, student.LastName, student.Phone,
                string.Join(", ", student.EnrolledCourses.Select(c => c.Name)), string.Join(", ", student.Grades.Select(g => $"{g.Key.Name}: {g.Value}")));

        return table;
    }

    public Table DataTable(string title, params Instructor[] instructors)
    {
        var table = new Table();

        table.Title(title.ToUpper())
            .BorderColor(Color.Blue)
            .AsciiBorder();

        table.AddColumn("ID");
        table.AddColumn("FirstName");
        table.AddColumn("LastName");
        table.AddColumn("Phone");
        table.AddColumn("Expertise");

        foreach (var instructor in instructors)
            table.AddRow(instructor.Id.ToString(), instructor.FirstName, instructor.LastName, instructor.Phone, instructor.Expertise);

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
        table.AddColumn("StartDate");
        table.AddColumn("EndDate");
        table.AddColumn("Instructor");
        table.AddColumn("Number of Students");

        foreach (var course in courses)
            table.AddRow(course.Id.ToString(), course.Name, course.Description, course.StartDate.ToString(), course.EndDate.ToString(), $"{course.Instructor.FirstName} {course.Instructor.LastName}", course.Students.Count.ToString());

        return table;
    }

    public Table DataTable(string title, params Enrollment[] enrollments)
    {
        var table = new Table();

        table.Title(title.ToUpper())
            .BorderColor(Color.Blue)
            .AsciiBorder();

        table.AddColumn("ID");
        table.AddColumn("CourseId");
        table.AddColumn("StudentId");

        foreach (var enrollment in enrollments)
            table.AddRow(enrollment.Id.ToString(), enrollment.CourseId.ToString(), enrollment.StudentId.ToString());

        return table;
    }

    public string ShowSelectionMenu(string title, string[] options)
    {
        var selection = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title(title)
                .PageSize(5) // Number of items visible at once
                .AddChoices(options)
                .HighlightStyle(new Style(foreground: Color.Cyan1, background: Color.Blue))
        );

        return selection;
    }
}

