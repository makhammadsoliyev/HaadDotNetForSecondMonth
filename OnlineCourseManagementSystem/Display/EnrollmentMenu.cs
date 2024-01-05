using OnlineCourseManagementSystem.Models;
using OnlineCourseManagementSystem.Services;
using Spectre.Console;

namespace OnlineCourseManagementSystem.Display;

public class EnrollmentMenu
{
    private readonly EnrollmentService enrollmentService;

    public EnrollmentMenu(EnrollmentService enrollmentService)
    {
        this.enrollmentService = enrollmentService;
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

        var enrollment = new Enrollment()
        {
            CourseId = courseId,
            StudentId = studentId
        };

        try
        {
            var addedEnrollment = enrollmentService.Add(enrollment);
            AnsiConsole.MarkupLine("[green]Successfully enrolled...[/]");
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
            var enrollment = enrollmentService.GetById(id);
            var table = new SelectionMenu().DataTable("Enrollment", enrollment);
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
            bool isDeleted = enrollmentService.Delete(id);
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
        var enrollments = enrollmentService.GetAll().ToArray();
        var table = new SelectionMenu().DataTable("Enrollments", enrollments);
        AnsiConsole.Write(table);
        AnsiConsole.MarkupLine("[blue]Enter to continue...[/]");
        Console.ReadKey();
    }

    public void Display()
    {
        var circle = true;
        var selectionDisplay = new SelectionMenu();

        while (circle)
        {
            AnsiConsole.Clear();
            var selection = selectionDisplay.ShowSelectionMenu("Choose one of options",
                new string[] { "Add", "GetById", "Delete", "GetAll", "Back" });

            switch (selection)
            {
                case "Add":
                    Add();
                    break;
                case "GetById":
                    GetById();
                    break;
                case "Delete":
                    Delete();
                    break;
                case "GetAll":
                    GetAll();
                    break;
                case "Back":
                    circle = false;
                    break;
            }
        }
    }
}
