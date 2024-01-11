using Spectre.Console;
using StudyTrackerSystem.Models;
using StudyTrackerSystem.Services;
using System.Text.RegularExpressions;

namespace StudyTrackerSystem.Display;

public class StudentMenu
{
    private readonly StudentService studentService;

    public StudentMenu(StudentService studentService)
    {
        this.studentService = studentService;
    }

    private void Add()
    {
        string firstName = AnsiConsole.Ask<string>("[blue]FirstName: [/]");
        string lastName = AnsiConsole.Ask<string>("[cyan2]LastName: [/]");
        string email = AnsiConsole.Ask<string>("[cyan1]Email: [/]");
        while (!Regex.IsMatch(email, @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$"))
        {
            AnsiConsole.MarkupLine("[red]Invalid input.[/]");
            email = AnsiConsole.Ask<string>("[cyan1]Email: [/]");
        }
        string password = AnsiConsole.Ask<string>("[yellow]Password: [/]");
        while (!Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()_+])[A-Za-z\d!@#$%^&*()_+]{8,}$"))
        {
            AnsiConsole.MarkupLine("[red]At least 8 characters in length\r\nAt least one uppercase letter\r\nAt least one lowercase letter\r\nAt least one digit\r\nAt least one special character from the set (!@#$%^&*()_+)[/]");
            password = AnsiConsole.Ask<string>("[yellow]Password: [/]");
        }

        var student = new Student()
        {
            Email = email,
            Password = password,
            LastName = lastName,
            FirstName = firstName
        };

        try
        {
            var addedStudent = studentService.Add(student);
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
            var student = studentService.GetById(id);
            var table = new SelectionMenu().DataTable("Student", student);
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
        string email = AnsiConsole.Ask<string>("[cyan1]Email: [/]");
        while (!Regex.IsMatch(email, @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$"))
        {
            AnsiConsole.MarkupLine("[red]Invalid input.[/]");
            email = AnsiConsole.Ask<string>("[cyan1]Email: [/]");
        }
        string password = AnsiConsole.Ask<string>("[yellow]Password: [/]");
        while (!Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()_+])[A-Za-z\d!@#$%^&*()_+]{8,}$"))
        {
            AnsiConsole.MarkupLine("[red]At least 8 characters in length\r\nAt least one uppercase letter\r\nAt least one lowercase letter\r\nAt least one digit\r\nAt least one special character from the set (!@#$%^&*()_+)[/]");
            password = AnsiConsole.Ask<string>("[yellow]Password: [/]");
        }

        var student = new Student()
        {
            Id = id,
            Email = email,
            Password = password,
            LastName = lastName,
            FirstName = firstName
        };

        try
        {
            var updatedStudent = studentService.Update(id, student);
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
            bool isDeleted = studentService.Delete(id);
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
        var students = studentService.GetAll().ToArray();
        var table = new SelectionMenu().DataTable("Students", students);
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
