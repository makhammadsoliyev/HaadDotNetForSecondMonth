using HospitalInformationSystem.Entities;
using HospitalInformationSystem.Services;
using Spectre.Console;

namespace HospitalInformationSystem.Menus;

public class MedicalRecordMenu
{
    private readonly MedicalRecordService medicalRecordService;

    public MedicalRecordMenu(MedicalRecordService medicalRecordService)
    {
        this.medicalRecordService = medicalRecordService;
    }

    private void Add()
    {
        int patientId = AnsiConsole.Ask<int>("[aqua]PatientId: [/]");
        while (patientId <= 0)
        {
            AnsiConsole.MarkupLine($"[red]Invalid input.[/]");
            patientId = AnsiConsole.Ask<int>("[aqua]PatientId: [/]");
        }
        int doctorId = AnsiConsole.Ask<int>("[aqua]DoctorId: [/]");
        while (doctorId <= 0)
        {
            AnsiConsole.MarkupLine($"[red]Invalid input.[/]");
            doctorId = AnsiConsole.Ask<int>("[blue]DoctorId: [/]");
        }
        string medicalConditions = AnsiConsole.Ask<string>("[blue]MedicalConditions: [/]");
        string medications = AnsiConsole.Ask<string>("[aqua]Medications: [/]");
        string testResults = AnsiConsole.Ask<string>("[aqua]TestResults: [/]");
        string treatmentPlans = AnsiConsole.Ask<string>("[aqua]TreatmentPlans: [/]");

        var record = new MedicalRecord()
        {
            DoctorId = doctorId,
            PatientId = patientId,
            TestResults = testResults,
            Medications = medications,
            TreatmentPlans = treatmentPlans,
            MedicalConditions = medicalConditions,
        };

        try
        {
            var addedRecord = medicalRecordService.Add(record);
            AnsiConsole.MarkupLine("[green]Successfully deleted...[/]");
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
            var record = medicalRecordService.GetById(id);
            var table = new SelectionMenu().DataTable("Record", record);
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
        int patientId = AnsiConsole.Ask<int>("[aqua]PatientId: [/]");
        while (patientId <= 0)
        {
            AnsiConsole.MarkupLine($"[red]Invalid input.[/]");
            patientId = AnsiConsole.Ask<int>("[aqua]PatientId: [/]");
        }
        int doctorId = AnsiConsole.Ask<int>("[aqua]DoctorId: [/]");
        while (doctorId <= 0)
        {
            AnsiConsole.MarkupLine($"[red]Invalid input.[/]");
            doctorId = AnsiConsole.Ask<int>("[blue]DoctorId: [/]");
        }
        string medicalConditions = AnsiConsole.Ask<string>("[blue]MedicalConditions: [/]");
        string medications = AnsiConsole.Ask<string>("[aqua]Medications: [/]");
        string testResults = AnsiConsole.Ask<string>("[aqua]TestResults: [/]");
        string treatmentPlans = AnsiConsole.Ask<string>("[aqua]TreatmentPlans: [/]");

        var record = new MedicalRecord()
        {
            Id = id,
            DoctorId = doctorId,
            PatientId = patientId,
            TestResults = testResults,
            Medications = medications,
            TreatmentPlans = treatmentPlans,
            MedicalConditions = medicalConditions,
        };

        try
        {
            var updatedRecord = medicalRecordService.Update(id, record);
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
            bool isDeleted = medicalRecordService.Delete(id);
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
        var records = medicalRecordService.GetAll().ToArray();
        var table = new SelectionMenu().DataTable("Records", records);
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
