using OnlineCourseManagementSystem.Models;
using OnlineCourseManagementSystem.Services;
using Spectre.Console;
using System.Text.RegularExpressions;

namespace OnlineCourseManagementSystem.Display;

public class InstructorMenu
{
    private readonly InstructorService instructorService;

    public InstructorMenu(InstructorService instructorService)
    {
        this.instructorService = instructorService;
    }

    private void Add()
    {
        string firstName = AnsiConsole.Ask<string>("[blue]FirstName: [/]").Trim();
        string lastName = AnsiConsole.Ask<string>("[cyan2]LastName: [/]").Trim();
        string phone = AnsiConsole.Ask<string>("[cyan1]Phone(+998XXxxxxxxx): [/]");
        while (!Regex.IsMatch(phone, @"^\+998\d{9}$"))
        {
            AnsiConsole.MarkupLine("[red]Invalid input.[/]");
            phone = AnsiConsole.Ask<string>("[cyan1]Phone(+998XXxxxxxxx): [/]");
        }
        string expertise = AnsiConsole.Ask<string>("[blue]Expertise: [/]");

        var instructor = new Instructor()
        {
            Phone = phone,
            LastName = lastName,
            FirstName = firstName,
            Expertise = expertise
        };

        try
        {
            var addedInstructor = instructorService.Add(instructor);
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
            var instructor = instructorService.GetById(id);
            var table = new SelectionMenu().DataTable("Instructor", instructor);
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
        string firstName = AnsiConsole.Ask<string>("[blue]FirstName: [/]");
        string lastName = AnsiConsole.Ask<string>("[cyan2]LastName: [/]");
        string phone = AnsiConsole.Ask<string>("[cyan1]Phone(+998XXxxxxxxx): [/]");
        while (!Regex.IsMatch(phone, @"^\+998\d{9}$"))
        {
            AnsiConsole.MarkupLine("[red]Invalid input.[/]");
            phone = AnsiConsole.Ask<string>("[cyan1]Phone(+998XXxxxxxxx): [/]");
        }
        string expertise = AnsiConsole.Ask<string>("[blue]Expertise: [/]");

        var instructor = new Instructor()
        {
            Id = id,
            Phone = phone,
            LastName = lastName,
            FirstName = firstName,
            Expertise = expertise
        };

        try
        {
            var updatedInstructor = instructorService.Update(id, instructor);
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
            bool isDeleted = instructorService.Delete(id);
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
        var instructors = instructorService.GetAll().ToArray();
        var table = new SelectionMenu().DataTable("Instructors", instructors);
        AnsiConsole.Write(table);
        AnsiConsole.MarkupLine("[blue]Enter to continue...[/]");
        Console.ReadKey();
    }

    private void GradeAssignment()
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

        decimal grade = AnsiConsole.Ask<decimal>("[yellow]Grade(1-100): [/]");
        while (grade <= 0 || grade > 100)
        {
            AnsiConsole.MarkupLine($"[red]Invalid input.[/]");
            grade = AnsiConsole.Ask<decimal>("[yellow]Grade(1-100): [/]");
        }

        try
        {
            var student = instructorService.GradeAssignment(studentId, courseId, grade);
            AnsiConsole.MarkupLine("[green]Successfully graded...[/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
        }
        Thread.Sleep(1500);
    }

    public void Display()
    {
        var circle = true;
        var selectionDisplay = new SelectionMenu();

        while (circle)
        {
            AnsiConsole.Clear();
            var selection = selectionDisplay.ShowSelectionMenu("Choose one of options",
                new string[] { "Add", "GetById", "Update", "Delete", "GetAll", "GradeAssignment", "Back" });

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
                case "GradeAssignment":
                    GradeAssignment();
                    break;
                case "Back":
                    circle = false;
                    break;
            }
        }
    }
}
