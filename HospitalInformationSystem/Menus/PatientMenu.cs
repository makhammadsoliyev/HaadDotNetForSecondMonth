using HospitalInformationSystem.Entities;
using HospitalInformationSystem.Enums;
using HospitalInformationSystem.Services;
using Spectre.Console;
using System.Text.RegularExpressions;

namespace HospitalInformationSystem.Menus;

public class PatientMenu
{
    private readonly PatientService patientService;

    public PatientMenu(PatientService patientService)
    {
        this.patientService = patientService;
    }

    private void Add()
    {
        string firstName = AnsiConsole.Ask<string>("[blue]FirstName: [/]");
        string lastName = AnsiConsole.Ask<string>("[cyan2]LastName: [/]");
        DateOnly dateOfBirth = AnsiConsole.Ask<DateOnly>("[cyan2]DateOfBirth(dd/mm/yyyy)): [/]");
        string gender = AnsiConsole.Ask<string>("[yellow]Gender(0. Male, 1. Female): [/]");
        while (gender != "0" && gender != "1")
        {
            AnsiConsole.MarkupLine("[red]Invalid input.[/]");
            gender = AnsiConsole.Ask<string>("[yellow]Gender(0. Male, 1. Female): [/]");
        }
        string phone = AnsiConsole.Ask<string>("[cyan1]Phone(+998XXxxxxxxx): [/]");
        while (!Regex.IsMatch(phone, @"^\+998\d{9}$"))
        {
            AnsiConsole.MarkupLine("[red]Invalid input.[/]");
            phone = AnsiConsole.Ask<string>("[cyan1]Phone(+998XXxxxxxxx): [/]");
        }
        string address = AnsiConsole.Ask<string>("[cyan3]Address: [/]");
        string medicalHistory = AnsiConsole.Ask<string>("[blue]MedicalHistory: [/]");

        var patient = new Patient()
        {
            Phone = phone,
            Address = address,
            LastName = lastName,
            FirstName = firstName,
            Gender = (Gender)int.Parse(gender),
            DateOfBirth = dateOfBirth,
            MedicalHistory = medicalHistory
        };

        try
        {
            var addedPatient = patientService.Add(patient);
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
            var patient = patientService.GetById(id);
            var table = new SelectionMenu().DataTable("Patient", patient);
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
        DateOnly dateOfBirth = AnsiConsole.Ask<DateOnly>("[cyan2]DateOfBirth(dd/mm/yyyy)): [/]");
        int gender = AnsiConsole.Ask<int>("[yellow]Gender: [/]");
        while (gender != 0 || gender != 1)
        {
            AnsiConsole.MarkupLine("[red]Invalid input.[/]");
            gender = AnsiConsole.Ask<int>("[yellow]Gender: [/]");
        }
        string phone = AnsiConsole.Ask<string>("[cyan1]Phone(+998XXxxxxxxx): [/]");
        while (!Regex.IsMatch(phone, @"^\+998\d{9}$"))
        {
            AnsiConsole.MarkupLine("[red]Invalid input.[/]");
            phone = AnsiConsole.Ask<string>("[cyan1]Phone(+998XXxxxxxxx): [/]");
        }
        string address = AnsiConsole.Ask<string>("[cyan3]Address: [/]");
        string medicalHistory = AnsiConsole.Ask<string>("[blue]Address: [/]");

        var patient = new Patient()
        {
            Id = id,
            Phone = phone,
            Address = address,
            LastName = lastName,
            FirstName = firstName,
            Gender = (Gender)gender,
            DateOfBirth = dateOfBirth,
            MedicalHistory = medicalHistory
        };

        try
        {
            var updatedPatient = patientService.Update(id, patient);
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
            bool isDeleted = patientService.Delete(id);
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
        var patients = patientService.GetAll().ToArray();
        var table = new SelectionMenu().DataTable("Patients", patients);
        AnsiConsole.Write(table);
        AnsiConsole.MarkupLine("[blue]Enter to continue...[/]");
        Console.ReadKey();
    }

    private void GetByName()
    {
        string fullName = AnsiConsole.Ask<string>("[blue]FullName(FirstName LastName): [/]");
        var patients = patientService.FindAllByName(fullName).ToArray();
        var table = new SelectionMenu().DataTable("Patients", patients);
        AnsiConsole.Write(table);
        AnsiConsole.MarkupLine("[blue]Enter to continue...[/]");
        Console.ReadKey();
    }

    private void GetByAge()
    {
        uint age = AnsiConsole.Ask<uint>("[yellow]Age: [/]");
        var patients = patientService.FindAllByAge(age).ToArray();
        var table = new SelectionMenu().DataTable("Patients", patients);
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
                new string[] { "Add", "GetById", "Update", "Delete", "GetAll", "SearchByName", "SearchByAge", "Back" });

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
                case "SearchByAge":
                    GetByAge();
                    break;
                case "Back":
                    circle = false;
                    break;
            }
        }
    }
}
