using Spectre.Console;
using StudyTrackerSystem.Models;
using StudyTrackerSystem.Services;

namespace StudyTrackerSystem.Display;

public class StudySessionMenu
{
    private readonly StudySessionService studySessionService;

    public StudySessionMenu(StudySessionService studySessionService)
    {
        this.studySessionService = studySessionService;
    }

    private void Add()
    {
        int studentId = AnsiConsole.Ask<int>("[aqua]StudentId: [/]");
        while (studentId <= 0)
        {
            AnsiConsole.MarkupLine($"[red]Invalid input.[/]");
            studentId = AnsiConsole.Ask<int>("[aqua]StudentId: [/]");
        }
        int courseId = AnsiConsole.Ask<int>("[blue]CourseId: [/]");
        while (courseId <= 0)
        {
            AnsiConsole.MarkupLine($"[red]Invalid input.[/]");
            courseId = AnsiConsole.Ask<int>("[blue]CourseId: [/]");
        }
        DateOnly startDate = AnsiConsole.Ask<DateOnly>("[cyan1]StartDate: [/]");
        int durationInMinutes = AnsiConsole.Ask<int>("[aqua]DurationInMinutes: [/]");
        while (durationInMinutes <= 0)
        {
            AnsiConsole.MarkupLine($"[red]Invalid input.[/]");
            durationInMinutes = AnsiConsole.Ask<int>("[aqua]Id: [/]");
        }
        string studyMaterialsCovered = AnsiConsole.Ask<string>("[yellow]StudyMaterialsCovered: [/]");
        string notes = AnsiConsole.Ask<string>("[cyan2]Notes: [/]");

        var studySession = new StudySession()
        {
            Notes = notes,
            CourseId = courseId,
            StudentId = studentId,
            StartDate = startDate,
            DurationInMinutes = durationInMinutes,
            StudyMaterialsCovered = studyMaterialsCovered
        };

        try
        {
            var addedStudySession = studySessionService.Add(studySession);
            AnsiConsole.MarkupLine("[green]Successfully added...[/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
        }
        Thread.Sleep(1500);
    }

    private void GetById()
    {
        int id = AnsiConsole.Ask<int>("[aqua]Id: [/]");
        while (id <= 0)
        {
            AnsiConsole.MarkupLine($"[red]Invalid input.[/]");
            id = AnsiConsole.Ask<int>("[aqua]Id: [/]");
        }

        try
        {
            var studySession = studySessionService.GetById(id);
            var table = new SelectionMenu().DataTable("StudySession", studySession);
            AnsiConsole.Write(table);
            AnsiConsole.MarkupLine("[blue]Enter to continue...[/]");
            Console.ReadKey();
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
            Thread.Sleep(1500);
        }
    }

    private void Update()
    {
        int id = AnsiConsole.Ask<int>("[aqua]Id: [/]");
        while (id <= 0)
        {
            AnsiConsole.MarkupLine($"[red]Invalid input.[/]");
            id = AnsiConsole.Ask<int>("[aqua]Id: [/]");
        }
        int studentId = AnsiConsole.Ask<int>("[aqua]StudentId: [/]");
        while (studentId <= 0)
        {
            AnsiConsole.MarkupLine($"[red]Invalid input.[/]");
            studentId = AnsiConsole.Ask<int>("[aqua]StudentId: [/]");
        }
        int courseId = AnsiConsole.Ask<int>("[blue]CourseId: [/]");
        while (courseId <= 0)
        {
            AnsiConsole.MarkupLine($"[red]Invalid input.[/]");
            courseId = AnsiConsole.Ask<int>("[blue]CourseId: [/]");
        }
        DateOnly startDate = AnsiConsole.Ask<DateOnly>("[cyan1]StartDate: [/]");
        int durationInMinutes = AnsiConsole.Ask<int>("[aqua]DurationInMinutes: [/]");
        while (durationInMinutes <= 0)
        {
            AnsiConsole.MarkupLine($"[red]Invalid input.[/]");
            durationInMinutes = AnsiConsole.Ask<int>("[aqua]Id: [/]");
        }
        string studyMaterialsCovered = AnsiConsole.Ask<string>("[yellow]StudyMaterialsCovered: [/]");
        string notes = AnsiConsole.Ask<string>("[cyan2]Notes: [/]");

        var studySession = new StudySession()
        {
            Id = id,
            Notes = notes,
            CourseId = courseId,
            StudentId = studentId,
            StartDate = startDate,
            DurationInMinutes = durationInMinutes,
            StudyMaterialsCovered = studyMaterialsCovered
        };

        try
        {
            var updatedStudySession = studySessionService.Update(id, studySession);
            AnsiConsole.MarkupLine("[green]Successfully updated...[/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
        }
        Thread.Sleep(1500);
    }

    private void Delete()
    {
        int id = AnsiConsole.Ask<int>("[aqua]Id: [/]");
        while (id <= 0)
        {
            AnsiConsole.MarkupLine($"[red]Invalid input.[/]");
            id = AnsiConsole.Ask<int>("[aqua]Id: [/]");
        }
        try
        {
            bool isDeleted = studySessionService.Delete(id);
            AnsiConsole.MarkupLine("[green]Successfully deleted...[/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
        }
        Thread.Sleep(1500);
    }

    private void GetAll()
    {
        var sessions = studySessionService.GetAll().ToArray();
        var table = new SelectionMenu().DataTable("Study sessions", sessions);
        AnsiConsole.Write(table);
        AnsiConsole.MarkupLine("[blue]Enter to continue...[/]");
        Console.ReadKey();
    }

    private void GetStudySessionsByStudent()
    {
        int studentId = AnsiConsole.Ask<int>("[aqua]StudentId: [/]");
        while (studentId <= 0)
        {
            AnsiConsole.MarkupLine($"[red]Invalid input.[/]");
            studentId = AnsiConsole.Ask<int>("[aqua]StudentId: [/]");
        }

        try
        {
            var sessions = studySessionService.GetStudySessionsByStudent(studentId).ToArray();
            var table = new SelectionMenu().DataTable($"StudySessions of StudentId: {studentId}", sessions);
            AnsiConsole.Write(table);
            AnsiConsole.MarkupLine("[blue]Enter to continue...[/]");
            Console.ReadKey();
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
            Thread.Sleep(1500);
        }
    }

    private void GetUpcomingStudySession()
    {
        var sessions = studySessionService.GetUpcomingStudySession().ToArray();
        var table = new SelectionMenu().DataTable("UpcomingStudySessions", sessions);
        AnsiConsole.Write(table);
        AnsiConsole.MarkupLine("[blue]Enter to continue...[/]");
        Console.ReadKey();
    }

    private void GetStudySessionsByCourse()
    {
        int courseId = AnsiConsole.Ask<int>("[blue]CourseId: [/]");
        while (courseId <= 0)
        {
            AnsiConsole.MarkupLine($"[red]Invalid input.[/]");
            courseId = AnsiConsole.Ask<int>("[blue]CourseId: [/]");
        }

        try
        {
            var sessions = studySessionService.GetStudySessionsByCourse(courseId).ToArray();
            var table = new SelectionMenu().DataTable($"StudySessions of CourseId: {courseId}", sessions);
            AnsiConsole.Write(table);
            AnsiConsole.MarkupLine("[blue]Enter to continue...[/]");
            Console.ReadKey();
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
            Thread.Sleep(1500);
        }
    }

    public void Display()
    {
        var circle = true;
        var selectionDisplay = new SelectionMenu();

        while (circle)
        {
            AnsiConsole.Clear();
            var selection = selectionDisplay.ShowSelectionMenu("Choose one of options",
                new string[] { "Add", "GetById", "Update", "Delete", "GetAll", "GetStudySessionsByCourse", "GetStudySessionsByStudent", "GetUpcomingStudySession", "Back" });

            switch (selection)
            {
                case "Add":
                    Add();
                    break;
                case "GetById":
                    GetById();
                    break;
                case "Update":
                    Update();
                    break;
                case "Delete":
                    Delete();
                    break;
                case "GetAll":
                    GetAll();
                    break;
                case "GetStudySessionsByCourse":
                    GetStudySessionsByCourse();
                    break;
                case "GetStudySessionsByStudent":
                    GetStudySessionsByStudent();
                    break;
                case "GetUpcomingStudySession":
                    GetUpcomingStudySession();
                    break;
                case "Back":
                    circle = false;
                    break;
            }
        }
    }
}
