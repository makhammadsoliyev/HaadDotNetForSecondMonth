using Spectre.Console;
using StudyTrackerSystem.Models;
using StudyTrackerSystem.Services;

namespace StudyTrackerSystem.Display;

public class ProgressRecordMenu
{
    private readonly ProgressRecordService progressRecordService;

    public ProgressRecordMenu(ProgressRecordService progressRecordService)
    {
        this.progressRecordService = progressRecordService;
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
        string progressDetails = AnsiConsole.Ask<string>("[yellow]ProgressDetails: [/]");

        var progressRecord = new ProgressRecord()
        {
            CourseId = courseId,
            StudentId = studentId,
            ProgressDetails = progressDetails
        };

        var addedProgressPecord = progressRecordService.Add(progressRecord);
        AnsiConsole.MarkupLine("[green]Successfully added...[/]");
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
            var record = progressRecordService.GetById(id);
            var table = new SelectionMenu().DataTable("ProgressRecord", record);
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
        string progressDetails = AnsiConsole.Ask<string>("[yellow]ProgressDetails: [/]");

        var progressRecord = new ProgressRecord()
        {
            Id = id,
            CourseId = courseId,
            StudentId = studentId,
            ProgressDetails = progressDetails
        };

        try
        {
            var updatedProgressRecord = progressRecordService.Update(id, progressRecord);
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
            bool isDeleted = progressRecordService.Delete(id);
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
        var records = progressRecordService.GetAll().ToArray();
        var table = new SelectionMenu().DataTable("Progress records", records);
        AnsiConsole.Write(table);
        AnsiConsole.MarkupLine("[blue]Enter to continue...[/]");
        Console.ReadKey();
    }

    private void GetProgressRecordsByCourse()
    {
        int courseId = AnsiConsole.Ask<int>("[blue]CourseId: [/]");
        while (courseId <= 0)
        {
            AnsiConsole.MarkupLine($"[red]Invalid input.[/]");
            courseId = AnsiConsole.Ask<int>("[blue]CourseId: [/]");
        }

        try
        {
            var records = progressRecordService.GetProgressRecordsByCourse(courseId).ToArray();
            var table = new SelectionMenu().DataTable($"Progress records of CourseId: {courseId}", records);
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

    private void GetProgressRecordsByStudent()
    {
        int studentId = AnsiConsole.Ask<int>("[aqua]StudentId: [/]");
        while (studentId <= 0)
        {
            AnsiConsole.MarkupLine($"[red]Invalid input.[/]");
            studentId = AnsiConsole.Ask<int>("[aqua]StudentId: [/]");
        }

        try
        {
            var records = progressRecordService.GetProgressRecordsByStudent(studentId).ToArray();
            var table = new SelectionMenu().DataTable($"Progress records of StudentId: {studentId}", records);
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
                new string[] { "Add", "GetById", "Update", "Delete", "GetAll", "GetProgressRecordsByCourse", "GetProgressRecordsByStudent", "Back" });

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
                case "GetProgressRecordsByCourse":
                    GetProgressRecordsByCourse();
                    break;
                case "GetProgressRecordsByStudent":
                    GetProgressRecordsByStudent();
                    break;
                case "Back":
                    circle = false;
                    break;
            }
        }
    }
}
