using OnlineCourseManagementSystem.Models;
using OnlineCourseManagementSystem.Services;
using Spectre.Console;

namespace OnlineCourseManagementSystem.Display;

public class CourseMenu
{
    private readonly CourseService courseService;
    private readonly InstructorService instructorService;

    public CourseMenu(CourseService courseService, InstructorService instructorService)
    {
        this.courseService = courseService;
        this.instructorService = instructorService;
    }

    private void Add()
    {
        string name = AnsiConsole.Ask<string>("[blue]Name: [/]");
        string description = AnsiConsole.Ask<string>("[cyan2]Description: [/]");
        DateOnly startDate = AnsiConsole.Ask<DateOnly>("[cyan1]StartDate(mm/dd/yy): [/]");
        DateOnly endDate = AnsiConsole.Ask<DateOnly>("[cyan3]EndDate(mm/dd/yy): [/]");
        int instructorId = AnsiConsole.Ask<int>("[aqua]InstructorId: [/]");
        while (instructorId <= 0)
        {
            AnsiConsole.MarkupLine($"[red]Invalid input.[/]");
            instructorId = AnsiConsole.Ask<int>("[aqua]InstructorId: [/]");
        }

        try
        {
            var course = new Course()
            {
                Name = name,
                EndDate = endDate,
                StartDate = startDate,
                Description = description,
                Instructor = instructorService.GetById(instructorId)
            };

            var addedCourse = courseService.Add(course);
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
        DateOnly startDate = AnsiConsole.Ask<DateOnly>("[cyan1]StartDate(mm/dd/yy): [/]");
        DateOnly endDate = AnsiConsole.Ask<DateOnly>("[cyan3]EndDate(mm/dd/yy): [/]");
        int instructorId = AnsiConsole.Ask<int>("[aqua]InstructorId: [/]");
        while (instructorId <= 0)
        {
            AnsiConsole.MarkupLine($"[red]Invalid input.[/]");
            instructorId = AnsiConsole.Ask<int>("[aqua]InstructorId: [/]");
        }

        try
        {
            var course = new Course()
            {
                Id = id,
                Name = name,
                EndDate = endDate,
                StartDate = startDate,
                Description = description,
                Instructor = instructorService.GetById(instructorId)
            };

            var updatedCourse = courseService.Add(course);
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

    private void SearchCoursesByInstructorName()
    {
        string fullName = AnsiConsole.Ask<string>("[blue]FullName(First, Second): [/]").Trim();
        var courses = courseService.SearchCoursesByInstructorName(fullName).ToArray();
        var table = new SelectionMenu().DataTable($"Courses of {fullName}", courses);
        AnsiConsole.Write(table);
        AnsiConsole.MarkupLine("[blue]Enter to continue...[/]");
        Console.ReadKey();
    }

    private void EnrollmentSummary()
    {
        int id = AnsiConsole.Ask<int>("[aqua]CourseId: [/]");
        while (id <= 0)
        {
            AnsiConsole.MarkupLine($"[red]Invalid input.[/]");
            id = AnsiConsole.Ask<int>("[aqua]CourseId: [/]");
        }
        try
        {
            var students = courseService.EnrollmentSummary(id).ToArray();
            var table = new SelectionMenu().DataTable($"Students of {courseService.GetById(id).Name}", students);
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
                new string[] { "Add", "GetById", "Update", "Delete", "GetAll", "SearchCoursesByInstructorName", "EnrollmentSummary", "Back" });

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
                case "SearchCoursesByInstructorName":
                    SearchCoursesByInstructorName();
                    break;
                case "EnrollmentSummary":
                    EnrollmentSummary();
                    break;
                case "Back":
                    circle = false;
                    break;
            }
        }
    }
}
