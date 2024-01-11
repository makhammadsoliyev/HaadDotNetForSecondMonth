using Spectre.Console;
using StudyTrackerSystem.Models;
using StudyTrackerSystem.Services;

namespace StudyTrackerSystem.Display;

public class CourseMenu
{
    private readonly CourseService courseService;

    public CourseMenu(CourseService courseService)
    {
        this.courseService = courseService;
    }

    private void Add()
    {
        string name = AnsiConsole.Ask<string>("[blue]Name: [/]");
        string description = AnsiConsole.Ask<string>("[cyan2]Description: [/]");
        string instructorName = AnsiConsole.Ask<string>("[cyan3]InstructorName: [/]");
        string schedule = AnsiConsole.Ask<string>("[cyan2]Schedule: [/]");
        int credits = AnsiConsole.Ask<int>("[yellow]Credits: [/]");
        while (credits <= 0)
        {
            AnsiConsole.MarkupLine($"[red]Invalid input.[/]");
            credits = AnsiConsole.Ask<int>("[yellow]Credits: [/]");
        }

        var course = new Course()
        {
            Name = name,
            Credits = credits,
            Schedule = schedule,
            Description = description,
            InstructorName = instructorName
        };

        var addedCourse = courseService.Add(course);
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
            var course = courseService.GetById(id);
            var table = new SelectionMenu().DataTable("Course", course);
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
        string name = AnsiConsole.Ask<string>("[blue]Name: [/]");
        string description = AnsiConsole.Ask<string>("[cyan2]Description: [/]");
        string instructorName = AnsiConsole.Ask<string>("[cyan3]InstructorName: [/]");
        string schedule = AnsiConsole.Ask<string>("[cyan2]Schedule: [/]");
        int credits = AnsiConsole.Ask<int>("[yellow]Credits: [/]");
        while (credits <= 0)
        {
            AnsiConsole.MarkupLine($"[red]Invalid input.[/]");
            credits = AnsiConsole.Ask<int>("[yellow]Credits: [/]");
        }

        var course = new Course()
        {
            Id = id,
            Name = name,
            Credits = credits,
            Schedule = schedule,
            Description = description,
            InstructorName = instructorName
        };

        try
        {
            var updatedCourse = courseService.Update(id, course);
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
            bool isDeleted = courseService.Delete(id);
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
        var courses = courseService.GetAll().ToArray();
        var table = new SelectionMenu().DataTable("Courses", courses);
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
                new string[] { "Add", "GetById", "Update", "Delete", "GetAll", "Back" });

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
                case "Back":
                    circle = false;
                    break;
            }
        }
    }
}
