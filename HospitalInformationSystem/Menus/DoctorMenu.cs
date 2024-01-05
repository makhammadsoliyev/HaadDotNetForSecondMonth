using HospitalInformationSystem.Entities;
using HospitalInformationSystem.Services;
using Spectre.Console;
using System.Text.RegularExpressions;

namespace HospitalInformationSystem.Menus;

public class DoctorMenu
{
    private readonly DoctorService doctorService;

    public DoctorMenu(DoctorService doctorService)
    {
        this.doctorService = doctorService;
    }

    private void Add()
    {
        string firstName = AnsiConsole.Ask<string>("[blue]FirstName: [/]");
        string lastName = AnsiConsole.Ask<string>("[cyan2]LastName: [/]");
        string specialization = AnsiConsole.Ask<string>("[cyan3]Specialization: [/]");
        string phone = AnsiConsole.Ask<string>("[cyan1]Phone(+998XXxxxxxxx): [/]");
        while (!Regex.IsMatch(phone, @"^\+998\d{9}$"))
        {
            AnsiConsole.MarkupLine("[red]Invalid input.[/]");
            phone = AnsiConsole.Ask<string>("[cyan1]Phone(+998XXxxxxxxx): [/]");
        }
        string address = AnsiConsole.Ask<string>("[cyan1]Address: [/]");

        var doctor = new Doctor()
        {
            Phone = phone,
            Address = address,
            LastName = lastName,
            FirstName = firstName,
            Specialization = specialization
        };

        try
        {
            var addedDoctor = doctorService.Add(doctor);
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
            var doctor = doctorService.GetById(id);
            var table = new SelectionMenu().DataTable("Doctor", doctor);
            AnsiConsole.Write(table);
            var appointments = doctor.Appointments.ToArray();
            var appointmentsTable = new SelectionMenu().DataTable("Doctor's Appointments", appointments);
            AnsiConsole.Write(appointmentsTable);
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
        string specialization = AnsiConsole.Ask<string>("[cyan3]Specialization: [/]");
        string phone = AnsiConsole.Ask<string>("[cyan1]Phone(+998XXxxxxxxx): [/]");
        while (!Regex.IsMatch(phone, @"^\+998\d{9}$"))
        {
            AnsiConsole.MarkupLine("[red]Invalid input.[/]");
            phone = AnsiConsole.Ask<string>("[cyan1]Phone(+998XXxxxxxxx): [/]");
        }
        string address = AnsiConsole.Ask<string>("[cyan1]Address: [/]");

        var doctor = new Doctor()
        {
            Id = id,
            Phone = phone,
            Address = address,
            LastName = lastName,
            FirstName = firstName,
            Specialization = specialization
        };

        try
        {
            var updatedDoctor = doctorService.Update(id, doctor);
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
            bool isDeleted = doctorService.Delete(id);
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
        var doctors = doctorService.GetAll().ToArray();
        var table = new SelectionMenu().DataTable("Doctors", doctors);
        AnsiConsole.Write(table);
        AnsiConsole.MarkupLine("[blue]Enter to continue...[/]");
        Console.ReadKey();
    }

    private void GetByName()
    {
        string fullName = AnsiConsole.Ask<string>("[blue]FullName(FirstName LastName): [/]");
        var doctors = doctorService.GetAllByName(fullName).ToArray();
        var table = new SelectionMenu().DataTable("Doctors", doctors);
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
                new string[] { "Add", "GetById", "Update", "Delete", "GetAll", "SearchByName", "Back" });

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
                case "SearchByName":
                    GetByName();
                    break;
                case "Back":
                    circle = false;
                    break;
            }
        }
    }
}
